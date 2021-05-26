using System;
using System.Collections.Generic;
using System.Numerics;



namespace Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            UIManager uiManager = new UIManager();
            
            Console.CursorVisible = false;

            MainMenu mainMenu = MainMenu.Data;
            CRUD crud = CRUD.CreatePipe;
            KindOfCompany kind = KindOfCompany.Pangyo;
            ConsoleKeyInfo key;

            while(true)
            {
                Console.CursorVisible = false;
                
                if(Util.Instance().state == State.Main)
                {
                    uiManager.ShowMenu(mainMenu);
                    key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            mainMenu = uiManager.PressUpKey(mainMenu);
                            break;
                        case ConsoleKey.DownArrow:
                            mainMenu = uiManager.PressDownKey(mainMenu);
                            break;
                        case ConsoleKey.Enter:
                            {
                                uiManager.PressEnterKey(mainMenu);
                            }
                            break;
                    }
                }
                else if(Util.Instance().state == State.DataManagement)
                {
                    uiManager.ShowMenu(crud);
                    key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            crud = uiManager.PressUpKey(crud);
                            break;
                        case ConsoleKey.DownArrow:
                            crud = uiManager.PressDownKey(crud);
                            break;
                        case ConsoleKey.Enter:
                            uiManager.PressEnterKey(crud);
                            break;
                        case ConsoleKey.Escape:
                            Util.Instance().state = State.Main;
                            break;
                    }
                }
                else if(Util.Instance().state == State.LoadMenu)
                {
                    uiManager.ShowMenu(kind);
                    key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            kind = uiManager.PressUpKey(kind);
                            break;
                        case ConsoleKey.DownArrow:
                            kind = uiManager.PressDownKey(kind);
                            break;
                        case ConsoleKey.Enter:
                            uiManager.PressEnterKey(kind);
                            break;
                        case ConsoleKey.Escape:
                            Util.Instance().state = State.DataManagement;
                            break;
                    }
                }
            }
        }        
    }
}
