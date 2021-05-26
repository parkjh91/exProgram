﻿using System;
using System.Text;
using System.Numerics;
using System.Collections.Generic;

namespace Pipeline
{
    class UIManager
    {
        public Repository repository = new Repository();
        public Repositorys repositorys = new Repositorys();

        // 메인화면 출력하는 함수
        public void ShowMenu(MainMenu mainMenu)
        {
            string data = "데이터 관리";
            string searchObstName = "관종 검색";
            string searchDia = "관경으로 탐색";
            string exit = "프로그램 종료";

            switch (mainMenu)
            {
                case MainMenu.Data:
                    data = $"{data} ◀";
                    break;
                case MainMenu.SearchObstName:
                    searchObstName = $"{searchObstName} ◀";
                    break;
                case MainMenu.SearchDia:
                    searchDia = $"{searchDia} ◀";
                    break;
                case MainMenu.Exit:
                    exit = $"{exit} ◀";
                    break;
            }

            Console.Clear();
            Console.WriteLine(data);
            Console.WriteLine(searchObstName);
            Console.WriteLine(searchDia);
            Console.WriteLine(exit);
        }


        public void ShowMenu(CRUD crud)
        {
            string create = "생성하기";
            string list = "파이프 정보";
            string modify = "파이프 수정";
            string delete = "파이프 삭제";
            string loadFile = "파일 불러오기";            

            switch (crud)
            {
                case CRUD.CreatePipe:
                    create = $"{create} ◀";
                    break;
                case CRUD.PipeList:
                    list = $"{list} ◀";
                    break;
                case CRUD.PipeModify:
                    modify = $"{modify} ◀";
                    break;
                case CRUD.PipeDelete:
                    delete = $"{delete} ◀";
                    break;
                case CRUD.LoadFile:
                    loadFile = $"{loadFile} ◀";
                    break;
            }

            Console.Clear();
            Console.WriteLine(create);
            Console.WriteLine(list);
            Console.WriteLine(modify);
            Console.WriteLine(delete);
            Console.WriteLine(loadFile);
        }

        public void ShowMenu(KindOfCompany kind)
        {
            string pangyo = "판교";
            string lg = "LG";

            switch(kind)
            {
                case KindOfCompany.Pangyo:
                    {
                        pangyo = $"{pangyo} ◀";
                    }
                    break;
                case KindOfCompany.LG:
                    {
                        lg = $"{lg} ◀";
                    }
                    break;
            }

            Console.Clear();
            Console.WriteLine(pangyo);
            Console.WriteLine(lg);
        }

        // 프로그램안에서 윗 방향키를 누른 경우
        public MainMenu PressUpKey(MainMenu mainMenu)
        {
            mainMenu--;
            if ((int)mainMenu == -1) mainMenu = MainMenu.Exit;

            return mainMenu;
        }

        public CRUD PressUpKey(CRUD crud)
        {
            crud--;
            if ((int)crud == -1) crud = CRUD.LoadFile;

            return crud;
        }

        public KindOfCompany PressUpKey(KindOfCompany kind)
        {
            kind--;

            if ((int)kind == -1) kind = KindOfCompany.LG;

            return kind;
        }

        // 프로그램안에서 아랫 방향키를 누른 경우
        public MainMenu PressDownKey(MainMenu mainMenu)
        {
            mainMenu++;

            if (mainMenu == MainMenu.Max) mainMenu = MainMenu.Data;

            return mainMenu;
        }

        public CRUD PressDownKey(CRUD crud)
        {
            crud++;

            if (crud == CRUD.Max) crud = CRUD.CreatePipe;

            return crud;
        }

        public KindOfCompany PressDownKey(KindOfCompany kind)
        {
            kind++;

            if (kind == KindOfCompany.Max) kind = KindOfCompany.Pangyo;

            return kind;
        }

        // 프로그램안에서 Enter키를 누른 경우
        public void PressEnterKey(MainMenu mainMenu)
        {
            switch(mainMenu)
            {
                case MainMenu.Data:
                    Util.Instance().state = State.DataManagement;
                    break;
                case MainMenu.SearchObstName:
                    //repository.SearchObstName();
                    RequestSameName();
                    break;
                case MainMenu.SearchDia:
                    repository.SearchDia();
                    break;
                case MainMenu.Exit:
                    Environment.Exit(0);
                    break;
            }
        }

        public void PressEnterKey(CRUD crud)
        {
            switch (crud)
            {
                case CRUD.CreatePipe:
                    InputPipeInfo();
                    break;
                case CRUD.PipeList:
                    //repository.EnterPipeList();
                    ShowPipeInfo();
                    break;
                case CRUD.PipeModify:
                    //repository.EnterPipeModify();
                    SendModifyValues();
                    break;
                case CRUD.PipeDelete:
                    //repository.EnterPipeDelete();
                    RequestDelete();
                    break;
                case CRUD.LoadFile:
                    {
                        Util.Instance().state = State.LoadMenu;
                        ShowMenu(KindOfCompany.Pangyo);
                    }
                    break;
            }
        }

        public void PressEnterKey(KindOfCompany kind)
        {
            repositorys.LoadJson(kind);

            Console.Clear();
            Console.WriteLine("불러오기가 성공하였습니다.");
            Console.ReadLine();

            Util.Instance().state = State.DataManagement;
        }

        public void InputPipeInfo()
        {
            string id;
            string[] strPos;
            int[] pos;
            Vector3 startPos;
            Vector3 endPos;
            string name;
            float dia;
            string color;

            Console.Clear();
            while (true)
            {
                Console.WriteLine("ID를 입력바랍니다.");
                id = Console.ReadLine();
                if (repositorys.CheckID(id)) break;
                Console.WriteLine("일치하는 ID가 있습니다.");
            }

            Console.WriteLine("시작 좌표 X, Y, Z의 값을 입력바랍니다.");
            strPos = Console.ReadLine().Split(' ');
            pos = Util.Instance().CheckEx(strPos);
            startPos = new Vector3(pos[0], pos[1], pos[2]);

            Console.WriteLine("끝 좌표 X, Y, Z의 값을 입력바랍니다.");
            strPos = Console.ReadLine().Split(' ');
            pos = Util.Instance().CheckEx(strPos);
            endPos = new Vector3(pos[0], pos[1], pos[2]);

            Console.WriteLine("관종을 입력바랍니다.");
            name = Console.ReadLine();

            Console.WriteLine("관경을 입력바랍니다.");
            dia = Util.Instance().CheckEx(Console.ReadLine());

            Console.WriteLine("색깔을 입력바랍니다.");
            color = Console.ReadLine();

            repositorys.CreatePipe(id, startPos, endPos, name, dia, color);

            Console.Clear();
            Console.WriteLine("생성되었습니다.");
            Console.ReadLine();
        }

        public void ShowPipeInfo()
        {
            List<Pipeline> pipes = repositorys.GetPipeInfo();
            Console.Clear();

            if(pipes.Count == 0)
            {
                Console.WriteLine("입력된 파이프 정보가 없습니다.");
                Console.ReadLine();
                return;
            }

            for(int i = 0; i < pipes.Count; i++)
            {
                Console.WriteLine(pipes[i].ToString());
            }
            Console.ReadLine();
        }

        public void SendModifyValues()
        {
            string id;
            string[] strPos;
            int[] pos;
            Vector3 startPos;
            Vector3 endPos;
            string name;
            float dia;
            string color;

            Console.Clear();
            while(true)
            {
                Console.WriteLine("수정하실 파이프의 ID를 입력하세요.");
                id = Console.ReadLine();
                if (!repositorys.CheckID(id)) break; // 일치하는 id가 없으면 true
                Console.WriteLine("일치하는 ID가 없습니다.");
            }

            Console.WriteLine("시작 좌표 X, Y, Z의 값을 입력바랍니다.");
            strPos = Console.ReadLine().Split(' ');
            pos = Util.Instance().CheckEx(strPos);
            startPos = new Vector3(pos[0], pos[1], pos[2]);

            Console.WriteLine("끝 좌표 X, Y, Z의 값을 입력바랍니다.");
            strPos = Console.ReadLine().Split(' ');
            pos = Util.Instance().CheckEx(strPos);
            endPos = new Vector3(pos[0], pos[1], pos[2]);

            Console.WriteLine("관종을 입력바랍니다.");
            name = Console.ReadLine();

            Console.WriteLine("관경을 입력바랍니다.");
            dia = Util.Instance().CheckEx(Console.ReadLine());

            Console.WriteLine("색깔을 입력바랍니다.");
            color = Console.ReadLine();

            repositorys.ModifyPipe(id, startPos, endPos, name, dia, color);
            Console.Clear();
            Console.WriteLine("수정이 완료되었습니다.");
            Console.ReadLine();
        }

        public void RequestDelete()
        {
            string id;

            Console.Clear();
            while(true)
            {
                Console.WriteLine("삭제하실 파이프의 ID를 입력바랍니다.");
                id = Console.ReadLine();
                if (repositorys.DeletePipe(id)) break;
                Console.WriteLine("입력하신 ID의 파이프 정보가 없습니다.");
            }

            Console.Clear();
            Console.WriteLine("삭제가 완료되었습니다.");
            Console.ReadLine();
        }

        private void RequestSameName()
        {
            string name;

            Console.Clear();
            Console.WriteLine("검색하실 관종을 입력바랍니다.");
            name = Console.ReadLine();

            List<Pipeline> pipes = repositorys.SearchSameName(name);

            if(pipes.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("입력하신 관종의 파이프 정보가 없습니다.");
                Console.ReadLine();
            }
            else
            {
                Console.Clear();
                for(int i = 0; i < pipes.Count; i++)
                {
                    Console.WriteLine(pipes[i].ToString());
                }
                Console.ReadLine();
            }
        }
    }
}