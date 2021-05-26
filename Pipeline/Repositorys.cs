using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Pipeline
{
    class Repositorys
    {
        List<Pipeline> pipelines = new List<Pipeline>();
        PipeProperty pipeProperty = new PipeProperty();
        List<PipeProperty> propertyList = new List<PipeProperty>();
        
        int count = 1;
        bool isFileOpenSucceed = true;

        public Repositorys()
        {
            SetPipeProperty();
        }

        // ID Check 일치하는 것이 있으면 false 없으면 true
        public bool CheckID(string id)
        {
            if (pipelines == null) return true;

            for(int i = 0; i < pipelines.Count; i++)
            {
                if (id == pipelines[i].PipeID) return false;
            }

            return true;
        }

        // 생성
        public void CreatePipe(string id, Vector3 startPos, Vector3 endPos, string obstName, float dia, string color)
        {
            Pipeline pipe = new Pipeline();

            pipe.PipeID = id;
            pipe.StartPosition = startPos;
            pipe.EndPosition = endPos;
            pipe.KindOfPipe = obstName;
            pipe.PipeDiameter = dia;
            pipe.PipeColor = color;
            pipe.TakeLength();
            pipe.PipeIndex = count++;
            pipelines.Add(pipe);
        }

        // 정보 반환
        public List<Pipeline> GetPipeInfo()
        {
            return pipelines;
        }

        // 수정
        public void ModifyPipe(string id, Vector3 startPos, Vector3 endPos, string obstName, float dia, string color)
        {
            for(int i = 0; i < pipelines.Count; i++)
            {
                if(id == pipelines[i].PipeID)
                {
                    pipelines[i].StartPosition = startPos;
                    pipelines[i].EndPosition = endPos;
                    pipelines[i].KindOfPipe = obstName;
                    pipelines[i].PipeDiameter = dia;
                    pipelines[i].PipeColor = color;
                    pipelines[i].TakeLength();
                }
            }
        }

        // 삭제
        public bool DeletePipe(string id)
        {
            if (CheckID(id)) return false;  // checkid -> 일치하는 id가 없으면 true

            int index = -1;

            for(int i = 0; i < pipelines.Count; i++)
            {
                if(id == pipelines[i].PipeID)
                {
                    pipelines.RemoveAt(i);
                    count--;
                    index = i;
                }
            }

            for(int j = index; j < pipelines.Count; j++)
            {
                pipelines[j].PipeIndex = j + 1;
            }

            return true;
        }

        // json 파일 불러와서 저장하기
        public void LoadJson(KindOfCompany kind)
        {
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
            foreach (JObject item in jsonDoc)
            {
                if (CheckID(item[pipeProperty.id].ToString()))
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
                    pipe.PipeIndex = count++;

                    pipelines.Add(pipe);
                }
            }
        }

        // 불러올 json형식의 파일 초기화
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
                return false;
            }

            return true;
        }

        // 관의 종류를 받아와 같은 종류의 관을 리턴함
        public List<Pipeline> SearchSameName(string name)
        {
            List<Pipeline> pipes = new List<Pipeline>();

            for (int i = 0; i < pipelines.Count; i++)
            {
                if (name == pipelines[i].KindOfPipe) pipes.Add(pipelines[i]);
            }

            return pipes;
        }

        // 관의 지름을 받아와 보다 큰 지름의 관을 리턴함
        public List<Pipeline> SearchOverDia(float dia)
        {
            List<Pipeline> pipes = new List<Pipeline>();

            for(int i = 0; i < pipelines.Count; i++)
            {
                if (dia < pipelines[i].PipeDiameter) pipes.Add(pipelines[i]);
            }

            return pipes;
        }
    }
}
