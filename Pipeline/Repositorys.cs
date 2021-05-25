using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Pipeline
{
    class Repositorys
    {
        List<Pipeline> pipelines = new List<Pipeline>();
        int count = 1;

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
    }
}
