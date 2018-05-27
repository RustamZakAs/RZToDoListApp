using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            RZJson RZSaveJson = new RZJson();
            RZAccountsList = (List<RZAccount>)RZSaveJson.Load("Users",1);
            RZTasksList = (List<RZTasks>)RZSaveJson.Load("Tasks",2);
            RZMainMenyu(ref RZAccountsList);
        }
        public static void RZMainMenyu (ref List<RZAccount> RZAccountsList)
        {
            if (/*ThisUser.Length > 0 ||*/ ThisUser != null) RZMainMenyuUser(ref RZAccountsList);
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
        public static void RZMainMenyuUser(ref List<RZAccount> RZAccountsList)
        {
            Console.SetBufferSize(75, 20);
            Console.SetWindowSize(75, 20);

            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Welcome, { ThisUser }");

            int m_ind = 0;
            int m_count = 5;
            var m_list = new string[m_count];
            m_list[0] = "  Add task    ";
            m_list[1] = "  My tasks     ";
            m_list[2] = "  Parameters  ";
            m_list[3] = "  Log out     ";
            m_list[4] = "  Exit        ";

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
                            foreach (var item in RZTasksList)
                            {
                                Console.WriteLine(item.ToString());
                            }
                            Console.ReadKey();
                            break;
                        case 2: //m_list[2] = "  Parameters  ";
                            RZParameters(ref RZAccountsList);
                            break;
                        case 3: //m_list[3] = "  Log out     ";

                            break;
                        case 4: //m_list[4] = "  Exit        ";
                            System.Environment.Exit(1);
                            break;
                    }
                }
            } while (true);
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
            RZJson RZSaveJson = new RZJson();
            RZSaveJson.Save(RZAccountsList, "Users");
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
            RZSaveJson.Save(RZTasksList, "Tasks");
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
