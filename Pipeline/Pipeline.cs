using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Pipeline
{
    class Pipeline
    {
        public string PipeID { get; set; }                  // 파이프 ID
        public Vector3 StartPosition { get; set; }          // 시작 좌표
        public Vector3 EndPosition { get; set; }            // 끝 좌표
        public double PipeLength { get; set; }              // 파이프의 길이
        public string KindOfPipe { get; set; }              // 파이프의 종류
        public float PipeDiameter { get; set; }               // 파이프의 지름
        public string PipeColor { get; set; }               // 파이프의 색깔
        public int PipeIndex { get; set; }                  // 출력 카운트를 위한 변수

        // 관의 길이 구하기
        public void TakeLength()
        {
            PipeLength = Vector3.Distance(StartPosition, EndPosition);
        }

        // 외부 오브젝트와 같은지 비교하기
        public override bool Equals(object obj)
        {
            Pipeline pipe = (Pipeline)obj;

            if (pipe == null) return false;

            if (PipeID != pipe.PipeID) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();            
        }

        // 관의 정보 출력
        public override string ToString()
        {
            string tempStr = null;

            //tempStr = $"{PipeNumber}번째 파이프 정보입니다. \n ID : {pipeID}\n 시작 좌표 : {StartPosition}\n 끝 좌표 : {EndPosition}\n 지장물 : {KindOfPipe}\n 관경 : {PipeDiameter}\n 관의 길이 : {PipeLength}\n 관의 색깔 : {PipeColor}\n";

            tempStr = $"{PipeIndex}번째 파이프 정보입니다. \n" +
                $"ID : {PipeID}\n" +
                $"시작 좌표 : {StartPosition}\n" +
                $"끝 좌표 : {EndPosition}\n" +
                $"지장물 : {KindOfPipe}\n" +
                $"관경 : {PipeDiameter}\n" +
                $"관의 길이 : {PipeLength,0:F}\n" +
                $"관의 색깔 : {PipeColor}\n";

            return tempStr;
        }
    }
}
