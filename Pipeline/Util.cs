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

        public State state = State.Main;        
    }
}
