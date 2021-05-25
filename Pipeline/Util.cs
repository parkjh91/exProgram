using System;
using System.Collections.Generic;
using System.Text;


public enum State
{
    Main,
    DataManagement,
    LoadMenu,
    Max
}

public enum MainMenu
{
    Data,
    SearchObstName,
    SearchDia,
    Exit,
    Max
}

public enum CRUD
{
    CreatePipe,
    PipeList,
    PipeModify,
    PipeDelete,
    LoadFile,
    Max
}

public enum KindOfCompany
{
    Pangyo,     // 판교
    LG,         // LG
    Max
}

namespace Pipeline
{
    class Util
    {
        private static Util util;

        public static Util Instance()
        {
            if(util == null)
            {
                util = new Util();
            }
            return util;
        }

        public int pipeCount = 1;

        public State state = State.Main;

        // string > int 컨버팅 예외처리 함수
        public float CheckEx(string value)
        {
            while (true)
            {
                try
                {
                    float returnValue;
                    return returnValue = float.Parse(value);
                }
                catch
                {
                    Console.WriteLine("숫자를 입력해 주세요.");
                    value = Console.ReadLine();
                }
            }
        }

        public int[] CheckEx(string[] values)
        {
            int[] pos = new int[3];

            while (true)
            {
                for (int i = 0; i < 3;)
                {
                    try
                    {
                        pos[i] = Convert.ToInt32(values[i]);
                        i++;
                    }
                    catch
                    {
                        Console.WriteLine("3개의 숫자를 입력해 주세요.");
                        values = Console.ReadLine().Split(' ');
                        i = 0;
                    }
                }

                if (values.Length != 3)
                {
                    Console.WriteLine("3개의 숫자를 입력해주세요.");
                    values = Console.ReadLine().Split(' ');
                }
                else
                {
                    break;
                }
            }

            return pos;
        }

        public int[] CheckEx(string[] values, List<Pipeline> pipelines)
        {
            int[] pos = new int[2];

            while (true)
            {
                for (int i = 0; i < 2;)
                {
                    try
                    {
                        pos[i] = Convert.ToInt32(values[i]);

                        if (pos[i] < 0 || pos[i] > pipelines.Count)
                        {
                            Console.WriteLine("인덱스 범위를 벗어났습니다. 다시 입력해주세요.");
                            values = Console.ReadLine().Split(' ');
                            i = 0;
                        }
                        else i++;
                    }
                    catch
                    {
                        Console.WriteLine("숫자를 입력해 주세요.");
                        values = Console.ReadLine().Split(' ');
                        i = 0;
                    }
                }

                if (values.Length != 2)
                {
                    Console.WriteLine("2개의 숫자를 입력해주세요.");
                    values = Console.ReadLine().Split(' ');
                }
                else
                {
                    break;
                }
            }

            return pos;
        }

        // 람다식
        //public int CheckEx(string value) => Convert.ToInt32(value);
    }
}
