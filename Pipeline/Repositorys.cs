using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Pipeline
{
    class Repositorys
    {
        List<Pipeline> pipelines = new List<Pipeline>();

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
            pipelines.Add(pipe);
        }
    }
}
