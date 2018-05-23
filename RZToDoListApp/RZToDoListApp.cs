using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RZToDoListApp
{
    class RZMain
    {
        public static string ThisUser { get; set; }
        static void Main(string[] args)
        {
            List<RZAccount> RZAccountsList;
            RZAccountsList = new List<RZAccount>();

            RZMainMenyu(ref RZAccountsList);
        }
        public static void RZMainMenyu (ref List<RZAccount> RZAccountsList)
        {
            Console.SetWindowSize(20, 11);
            Console.SetBufferSize(20, 11);
            //RZReadBin(ref info);
            //userCount = info.Length;

            int m_ind = 0;
            int m_count = 4;
            var m_list = new string[m_count];
            m_list[0] = "  Sign up     ";
            m_list[1] = "  Log in      ";
            m_list[2] = "  Parameters  ";
            m_list[3] = "  Exit        ";

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
                            break;
                        case 3:
                            System.Environment.Exit(1);
                            break;
                    }
                }
            } while (true);
        }
        private static void SetColorBlue()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
