using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace RZToDoListApp
{
    class RZMain
    {
        public static string ThisUser { get; set; }
        public static List<RZAccount> RZAccountsList;
        public static List<RZTasks> RZTasksList;
        static void Main(string[] args)
        {
            Console.Title = "To Do List";
            ThisUser = "";
            RZJson RZJsonx = new RZJson();
            RZAccountsList = (List<RZAccount>)RZJsonx.Load("Users",1);
            RZTasksList = (List<RZTasks>)RZJsonx.Load("Tasks",2);
            RZMainMenyu(ref RZAccountsList);
        }
        public static void RZMainMenyu (ref List<RZAccount> RZAccountsList)
        {
            if (ThisUser.Length != 0) RZMainMenyuUser(ref RZAccountsList);
            else
            {
                Console.SetWindowSize(20, 11);
                Console.SetBufferSize(20, 11);

                int m_ind = 0;
                int m_count = 3;
                var m_list = new string[m_count];
                m_list[0] = "  Sign up     ";
                m_list[1] = "  Log in      ";
                m_list[2] = "  Exit        ";

                ConsoleKeyInfo cki;
                do
                {
                    Console.Clear();
                    for (short i = 0; i < m_list.Length; i++)
                    {
                        if (m_ind == i)
                        {
                            SetColorBlue();
                            Console.WriteLine(m_list[i]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine(m_list[i]);
                        }
                    }
                    cki = Console.ReadKey();
                    //Console.WriteLine(cki.Key);
                    if (cki.Key == ConsoleKey.Escape)
                    {
                        System.Environment.Exit(1);
                    }
                    if (cki.KeyChar == 'a')
                    {
                        Console.WriteLine("You pressed a");
                    }
                    if (cki.Key == ConsoleKey.X &&
                    cki.Modifiers == ConsoleModifiers.Control)
                    {
                        Console.WriteLine("You pressed Ctrl+X");
                    }
                    if (cki.Key == ConsoleKey.DownArrow)
                    {
                        m_ind += 1;
                        if (m_ind >= m_count)
                        {
                            m_ind = m_count - 1;
                        }
                    }
                    if (cki.Key == ConsoleKey.UpArrow)
                    {
                        m_ind -= 1;
                        if (m_ind <= 0)
                        {
                            m_ind = 0;
                        }
                    }
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        switch (m_ind)
                        {
                            case 0:
                                RZAccount.RZSignUp(ref RZAccountsList);
                                break;
                            case 1:
                                if (RZAccountsList.Count == 0)
                                {
                                    Console.WriteLine("Users is not exist!");
                                    RZAccount.RZSignUp(ref RZAccountsList);  //if user is not then create new user 
                                }
                                else RZAccount.RZLogIn(ref RZAccountsList);
                                break;
                            case 2:
                                Environment.Exit(1);
                                break;
                        }
                    }
                } while (true);
            }
        }
        public static void ShowWelcome ()
        {
            Console.SetCursorPosition(23, 0);
            foreach (var item in RZAccountsList)
            {
                if (item.RZLogin == ThisUser)
                {
                    Console.WriteLine($"Welcome, { item.RZName } {item.RZSurname}");
                    break;
                }
            }
        }
        public static void RZMainMenyuUser(ref List<RZAccount> RZAccountsList)
        {
            Console.SetBufferSize(75, 120);
            Console.SetWindowSize(75, 20);
            ShowWelcome();

            int m_ind = 0;
            int m_count = 5;
            var m_list = new string[m_count];
            m_list[0] = "  Add task        ";
            m_list[1] = "  My tasks        ";
            m_list[2] = "  Parameters      ";
            m_list[3] = "  Log out         ";
            m_list[4] = "  Exit            ";

            ConsoleKeyInfo cki;
            do
            {
                Console.Clear();
                ShowWelcome();
                for (short i = 0; i < m_list.Length; i++)
                {
                    if (m_ind == i)
                    {
                        SetColorBlue();
                        Console.WriteLine(m_list[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(m_list[i]);
                    }
                }
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.DownArrow)
                {
                    m_ind += 1;
                    if (m_ind >= m_count)
                    {
                        m_ind = m_count - 1;
                    }
                }
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    m_ind -= 1;
                    if (m_ind <= 0)
                    {
                        m_ind = 0;
                    }
                }
                if (cki.Key == ConsoleKey.Enter)
                {
                    switch (m_ind)
                    {
                        case 0:  //m_list[0] = "  Add task    ";
                            RZTasks.RZAddTask(ref RZTasksList);
                            break;
                        case 1: //m_list[1] = "  My tasks     ";
                            RZMyTasks(ref RZTasksList);
                            break;
                        case 2: //m_list[2] = "  Parameters  ";
                            RZParameters(ref RZAccountsList);
                            break;
                        case 3: //m_list[3] = "  Log out     ";
                            ThisUser = "";
                            RZMainMenyu(ref RZAccountsList);
                            break;
                        case 4: //m_list[4] = "  Exit        ";
                            System.Environment.Exit(1);
                            break;
                    }
                }
            } while (true);
        }
        public static void RZMyTasks (ref List<RZTasks> RZTasksList)
        {
            ConsoleKeyInfo cki;
            List<int> indexs = new List<int>() {};
            for (int i = 0; i < RZTasksList.Count; i++)
            {
                if (RZTasksList[i].RZTUser == ThisUser)
                {
                    indexs.Add(i);
                }
            }

            int m_ind = 0;
            int m_count = 3;
            var m_list = new string[m_count];
            m_list[0] = "  Note completed  ";
            m_list[1] = "  Change task     ";
            m_list[2] = "  Delete task     ";

            int nowIndex = 0;
            do
            {
                Console.Clear();
                Console.WriteLine($"Найдено записей: {indexs.Count}");
                ShowWelcome();
                if(indexs.Count > 0) Console.WriteLine($"Запись номер: {nowIndex+1}");
                if (indexs.Count != 0) Console.WriteLine(RZTasksList[indexs[nowIndex]].ToString());
                else
                {
                    Console.ReadKey();
                    break;
                }
                cki = Console.ReadKey();

                if(indexs.Count > 0)
                {
                    if (cki.Key == ConsoleKey.DownArrow)
                    {
                        nowIndex += 1;
                        if (nowIndex >= indexs.Count)
                        {
                            nowIndex = indexs.Count - 1;
                        }
                    }
                    if (cki.Key == ConsoleKey.UpArrow)
                    {
                        nowIndex -= 1;
                        if (nowIndex <= 0)
                        {
                            nowIndex = 0;
                        }
                    }
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        int leftx = Console.CursorLeft;
                        int topx  = Console.CursorTop;
                        do
                        {
                            Console.SetCursorPosition(leftx,topx);
                            if (m_ind == 0)
                            {
                                SetColorBlue();
                                Console.Write($" {m_list[0]} ");
                                Console.ResetColor();
                                Console.Write($" {m_list[1]} ");
                                Console.Write($" {m_list[2]} ");

                            }
                            else if (m_ind == 1)
                            {
                                Console.Write($" {m_list[0]} ");
                                SetColorBlue();
                                Console.Write($" {m_list[1]} ");
                                Console.ResetColor();
                                Console.Write($" {m_list[2]} ");
                            }
                            else if (m_ind == 2)
                            {
                                Console.Write($" {m_list[0]} ");
                                Console.Write($" {m_list[1]} ");
                                SetColorBlue();
                                Console.Write($" {m_list[2]} ");
                                Console.ResetColor();
                            }
                            cki = Console.ReadKey();
                            if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.RightArrow)
                            {
                                m_ind += 1;
                                if (m_ind >= m_count)
                                {
                                    m_ind = m_count - 1;
                                }
                            }
                            if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.LeftArrow)
                            {
                                m_ind -= 1;
                                if (m_ind <= 0)
                                {
                                    m_ind = 0;
                                }
                            }
                            if (cki.Key == ConsoleKey.Enter)
                            {
                                Console.SetCursorPosition(leftx, topx + 1);
                                switch (m_ind)
                                {
                                    case 0:
                                        {
                                            Console.WriteLine($" *{m_list[0]} ");
                                            if (RZTasksList[indexs[nowIndex]].RZTDone == false)
                                            {
                                                RZTasksList[indexs[nowIndex]].RZTDone = true;
                                            }
                                            else RZTasksList[indexs[nowIndex]].RZTDone = false;
                                            RZMyTasks(ref RZTasksList);
                                        }
                                        break;
                                    case 1:
                                        {
                                            Console.WriteLine($" **{m_list[1]} ");
                                            Console.Write("Insert Title: ");
                                            RZTasksList[indexs[nowIndex]].RZTTitle = Console.ReadLine();
                                            RZJson RZJsonx = new RZJson();
                                            RZJsonx.Save(RZTasksList, "Tasks");
                                            RZMyTasks(ref RZTasksList);
                                        }
                                        break;
                                    case 2:
                                        {
                                            Console.WriteLine($" ***{m_list[2]} ");
                                            RZTasksList.RemoveAt(indexs[nowIndex]);
                                            indexs.RemoveAt(nowIndex);
                                            RZJson RZJsonx = new RZJson();
                                            RZJsonx.Save(RZTasksList, "Tasks");
                                            RZMyTasks(ref RZTasksList);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (cki.Key == ConsoleKey.Escape)
                            {
                                RZMyTasks(ref RZTasksList);
                            }
                        } while (true);
                    } // if cki.Key == ConsoleKey.Enter
                } //if indexs.Count > 0
            } while (cki.Key != ConsoleKey.Escape);
            RZMainMenyuUser(ref RZAccountsList);
        }
        private static void RZParameters (ref List<RZAccount> RZAccountsList)
        {
            Console.SetWindowSize(20, 11);
            Console.SetBufferSize(20, 11);

            int m_ind = 0;
            int m_count = 2;
            var m_list = new string[m_count];
            m_list[0] = "  Reset Database  ";
            m_list[1] = "      Back        ";

            ConsoleKeyInfo cki;
            do
            {
                Console.Clear();
                for (short i = 0; i < m_list.Length; i++)
                {
                    if (m_ind == i)
                    {
                        SetColorBlue();
                        Console.WriteLine(m_list[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(m_list[i]);
                    }
                }
                cki = Console.ReadKey();
                //Console.WriteLine(cki.Key);
                if (cki.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (cki.Key == ConsoleKey.DownArrow)
                {
                    m_ind += 1;
                    if (m_ind >= m_count)
                    {
                        m_ind = m_count - 1;
                    }
                }
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    m_ind -= 1;
                    if (m_ind <= 0)
                    {
                        m_ind = 0;
                    }
                }
                if (cki.Key == ConsoleKey.Enter)
                {
                    switch (m_ind)
                    {
                        case 0:
                            RZDatabaseRestore(ref RZAccountsList);
                            Console.ReadKey();
                            RZMain.Main(new string[] { });
                            break;
                        case 1:
                            RZMainMenyu(ref RZAccountsList);
                            break;
                    }
                }
            } while (true);
        }
        private static void RZDatabaseRestore(ref List<RZAccount> RZAccountsList)
        {
            for (int i = 0; i < RZAccountsList.Count; i++)
            {
                RZAccountsList.Remove(RZAccountsList[i]);
            }
            RZJson RZJsonx = new RZJson();
            RZJsonx.Save(RZAccountsList, "Users");
            if (File.Exists(@"Users.json"))
            {
                File.Delete(@"Users.json");
            }
            if (RZAccountsList.Count == 0) Console.WriteLine("Database is cleared/deleted");
            else Console.WriteLine("Error Database cleared/deleted!");
            //************************************************************
            for (int i = 0; i < RZAccountsList.Count; i++)
            {
                RZAccountsList.Remove(RZAccountsList[i]);
            }
            RZJsonx.Save(RZTasksList, "Tasks");
            if (File.Exists(@"Tasks.json"))
            {
                File.Delete(@"Tasks.json");
            }
            if (RZAccountsList.Count == 0) Console.WriteLine("Database is cleared/deleted");
            else Console.WriteLine("Error Database cleared/deleted!");
        }
        private static void SetColorBlue()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
