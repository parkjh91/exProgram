using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Pipeline
{
    struct PipeProperty
    {
        public string allText;
        public string id;
        public string obstName;
        public string position;
        public string color;
        public string pipeDia;
    }
    class Repository
    {
        PipeProperty pipeProperty = new PipeProperty();
        List<PipeProperty> propertyList = new List<PipeProperty>();
        List<Pipeline> pipelines = new List<Pipeline>();
        bool isFileOpenSucceed = true;

        // 관의 정보들 설정
        public void SettingPipe(Pipeline pipe)
        {
            // 람다 식 Func 연습
            Func<string, int> convertInt = value =>
            {
                while (true)
                {
                    try
                    {
                        return Convert.ToInt32(value);
                    }
                    catch
                    {
                        Console.WriteLine("숫자를 입력해 주세요.");
                        value = Console.ReadLine();
                    }
                }
            };

            Console.CursorVisible = true;
            int[] pos = new int[3];
            string[] posStr;

            Console.Clear();

            if (pipe.PipeIndex == 0) pipe.PipeIndex = Util.Instance().pipeCount;

            if (pipe.PipeID == null)
            {
                Console.WriteLine("{0}번관 ID = ", pipe.PipeIndex);
                pipe.PipeID = Console.ReadLine();

                for (int i = 0; i < pipelines.Count;)
                {
                    if (i == pipe.PipeIndex - 1)
                    {
                        i++;
                        continue;
                    }
                    if (pipe.PipeID == pipelines[i].PipeID)
                    {
                        Console.WriteLine("이미 존재하는 ID입니다. 다시 입력바랍니다.");
                        pipe.PipeID = Console.ReadLine();
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }
                }

                Util.Instance().pipeCount++;
            }

            Console.WriteLine("{0}번관 시작좌표 X, Y, Z = ", pipe.PipeIndex);
            posStr = Console.ReadLine().Split(' ');
            pos = Util.Instance().CheckEx(posStr);

            pipe.StartPosition = new Vector3(pos[0], pos[1], pos[2]);

            Console.WriteLine("{0}번관 끝좌표 X, Y, Z = ", pipe.PipeIndex);
            posStr = Console.ReadLine().Split(' ');
            pos = Util.Instance().CheckEx(posStr);

            pipe.EndPosition = new Vector3(pos[0], pos[1], pos[2]);
            pipe.TakeLength();

            Console.WriteLine("{0}번관 지장물 = ", pipe.PipeIndex);
            pipe.KindOfPipe = Console.ReadLine();

            Console.WriteLine("{0}번관의 지름 = ", pipe.PipeIndex);
            pipe.PipeDiameter = convertInt(Console.ReadLine());

            Console.WriteLine("{0}번관의 색깔 = ", pipe.PipeIndex);
            pipe.PipeColor = Console.ReadLine();            
        }

        // 파이프의 속성을 저장하는 함수
        public void EnterCreatePipe()
        {
            Pipeline pipe = new Pipeline();
            SettingPipe(pipe);
            pipelines.Add(pipe);
        }

        // 저장되어있는 파이프들의 목록을 보여주는 함수
        public void EnterPipeList()
        {
            if (pipelines.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("입력된 파이프 정보가 없습니다.");
                Console.ReadLine();
                return;
            }

            Console.Clear();

            for (int i = 0; i < pipelines.Count; i++)
            {
                Console.WriteLine(pipelines[i].ToString());
            }

            Console.ReadLine();
        }

        // 파이프 정보 수정하는 함수
        public void EnterPipeModify()
        {
            if (pipelines.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("입력된 파이프 정보가 없습니다.");
                Console.ReadLine();
                return;
            }
            Console.Clear();

            int modIndex = FindID();

            SettingPipe(pipelines[modIndex]);
        }

        // 파이프 정보 삭제하는 함수
        public void EnterPipeDelete()
        {
            if (pipelines.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("입력된 파이프 정보가 없습니다.");
                Console.ReadLine();
                return;
            }
            Console.Clear();

            int delIndex = FindID();

            pipelines.RemoveAt(delIndex);
            Util.Instance().pipeCount--;

            if (pipelines.Count == 0) return;
            for (int i = 0; i < pipelines.Count; i++)
            {
                pipelines[i].PipeIndex = i + 1;
            }
        }


        // 2개의 파이프를 입력받아 비교하는 함수
        public void EnterPipeEquals()
        {
            if (pipelines.Count < 2)
            {
                Console.Clear();
                Console.WriteLine("입력된 파이프 정보가 1개 이하입니다.");
                Console.ReadLine();
                return;
            }

            int[] pipeIndexs = new int[2];
            int firstIndex;
            int secondIndex;
            bool isSamePipe;
            string[] tempIndex;

            Console.Clear();
            Console.WriteLine("비교할 파이프의 인덱스 값 2개를 입력해 주세요.");
            Console.CursorVisible = true;

            tempIndex = Console.ReadLine().Split(' ');

            pipeIndexs = Util.Instance().CheckEx(tempIndex, pipelines);

            firstIndex = pipeIndexs[0] - 1;
            secondIndex = pipeIndexs[1] - 1;

            isSamePipe = pipelines[firstIndex].Equals(pipelines[secondIndex]);

            Console.Clear();

            Console.WriteLine(pipelines[firstIndex].ToString());
            Console.WriteLine(pipelines[secondIndex].ToString());

            Console.CursorVisible = false;
            if (isSamePipe)
            {
                Console.WriteLine("{0}번째 파이프와, {1}번째 파이프는 같습니다.", pipeIndexs[0], pipeIndexs[1]);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("{0}번째 파이프와, {1}번째 파이프는 같지 않습니다.", pipeIndexs[0], pipeIndexs[1]);
                Console.ReadLine();
            }
        }

        // ID값을 받아 파이프 List와 비교하여 index 반환
        private int FindID()
        {
            string id;
            int tempIndex = -1;
            Console.CursorVisible = true;

            Console.WriteLine("ID를 입력해 주세요.");
            id = Console.ReadLine();

            while (true)
            {
                for (int i = 0; i < pipelines.Count; i++)
                {
                    if (id == pipelines[i].PipeID)
                    {
                        tempIndex = i;
                        break;
                    }
                }

                if (tempIndex != -1) break;

                Console.WriteLine("해당하는 ID가 없습니다. 다시 입력 바랍니다.");
                id = Console.ReadLine();
            }

            return tempIndex;
        }

        private bool FindID(int id)
        {
            for(int i = 0; i < pipelines.Count; i++)
            {
                if (id.ToString() == pipelines[i].PipeID)
                    return false;
            }

            return true;
        }

        // json 파일 불러와서 저장하기
        public void LoadJson(KindOfCompany kind)
        {
            //SetPipeProperty(kind);
            pipeProperty = propertyList[(int)kind];

            if (!isFileOpenSucceed)
            {
                isFileOpenSucceed = true;
                return;
            }

            JArray jsonDoc = JArray.Parse(pipeProperty.allText);

            string posStr;
            string[] posStrArray;
            string[] posArray;
            Vector3[] vectors = new Vector3[2];
            float x;
            float y;
            float z;
            foreach(JObject item in jsonDoc)
            {
                if (FindID(int.Parse(item[pipeProperty.id].ToString())))
                {
                    Pipeline pipe = new Pipeline
                    {
                        PipeID = item[pipeProperty.id].ToString(),
                        KindOfPipe = item[pipeProperty.obstName].ToString(),
                        PipeColor = item[pipeProperty.color].ToString(),
                        PipeDiameter = float.Parse(item[pipeProperty.pipeDia].ToString())
                    };

                    posStr = Regex.Replace(item[pipeProperty.position].ToString(), @"[^0-9\.\,\s]", "");

                    posStrArray = posStr.Split("  ");
                    posStrArray = posStrArray[1].Split(',');

                    for (int i = 0; i < posStrArray.Length; i++)
                    {
                        posArray = posStrArray[i].Split(' ');
                        x = float.Parse(posArray[0]);
                        y = float.Parse(posArray[1]);
                        z = float.Parse(posArray[2]);

                        vectors[i] = new Vector3(x, y, z);
                    }

                    pipe.StartPosition = vectors[0];
                    pipe.EndPosition = vectors[1];
                    pipe.TakeLength();
                    pipe.PipeIndex = Util.Instance().pipeCount;
                    Util.Instance().pipeCount++;

                    pipelines.Add(pipe);
                }
            }

            Console.Clear();
            Console.WriteLine("불러오기가 성공하였습니다.");
            Console.ReadLine();
        }

        public void SetPipeProperty()
        {
            // Pangyo
            string path = @"./Pipeline Data.txt";

            isFileOpenSucceed = CheckFile(path);
            pipeProperty.allText = File.ReadAllText(path);
            pipeProperty.id = "linkId";
            pipeProperty.obstName = "obstName";
            pipeProperty.position = "geom";
            pipeProperty.pipeDia = "pipeDia";
            pipeProperty.color = "obstColor";

            propertyList.Add(pipeProperty);                    
                    
            // LG
            LGJson lgjson = new LGJson();

            pipeProperty.allText = lgjson.LGJsonAllText;
            pipeProperty.id = "gid";
            pipeProperty.obstName = "obstname";
            pipeProperty.position = "geom";
            pipeProperty.pipeDia = "pipe_dia";
            pipeProperty.color = "color";

            propertyList.Add(pipeProperty);
        }

        private bool CheckFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("파일을 불러오는데 실패했습니다.");
                Console.ReadLine();
                return false;
            }

            return true;
        }

        // 관의 종류를 입력받아 일치하는 관의 정보를 출력
        public void SearchObstName()
        {
            Console.Clear();
            Console.WriteLine("관종을 입력해 주세요.");

            string tempStr = Console.ReadLine();
            int count = 0;

            Console.Clear();
            for(int i = 0; i < pipelines.Count; i++)
            {
                if (tempStr != pipelines[i].KindOfPipe)
                {
                    count++;
                    continue;
                }

                Console.WriteLine(pipelines[i].ToString());
            }

            if(count == pipelines.Count)
            {
                Console.WriteLine("일치하는 파이프가 없습니다.");
                Console.ReadLine();
            }
            else
            {
                Console.ReadLine();
            }
        }

        // 관의 지름을 입력받아 입력받은 크기보다 큰 관의 정보 출력
        public void SearchDia()
        {
            Console.Clear();
            Console.WriteLine("관경을 입력해 주세요.");

            string tempStr = Console.ReadLine();
            float tempInt = Util.Instance().CheckEx(tempStr);
            int count = 0;

            Console.Clear();
            for (int i = 0; i < pipelines.Count; i++)
            {
                if (tempInt >= pipelines[i].PipeDiameter)
                {
                    count++;
                    continue;
                }

                Console.WriteLine(pipelines[i].ToString());
            }

            if (count == pipelines.Count)
            {
                Console.WriteLine("일치하는 파이프가 없습니다.");
                Console.ReadLine();
            }
            else
            {
                Console.ReadLine();
            }
        }
    }
}
