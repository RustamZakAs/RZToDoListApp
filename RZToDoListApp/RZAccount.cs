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
    [DataContract]
    class RZAccount
    {
        [DataMember]
        public string RZName { get; set; }
        [DataMember]
        public string RZSurname { get; set; }
        [DataMember]
        public int RZAge { get; set; }
        [DataMember]
        public string RZLogin { get; set; }
        [DataMember]
        public string RZPassword { get; set; }

        public static int userCount = 0;
        private const int password_len = 6;

        public override string ToString()
        {
            return $"{RZLogin}";
        }

        public static void SetColorBlue()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SetTextCoord(string text, int x, int y, int lenstr)
        {
            int i = 0, temp_len_str = lenstr;
            int Y = y;
            Console.SetCursorPosition(x, y);
            do
            {
                Console.Write(text[i]);
                i += 1;
                temp_len_str -= 1;
                if (temp_len_str == 0)
                {
                    Console.WriteLine();
                    temp_len_str = lenstr;
                    Y += 1;
                    Console.SetCursorPosition(x, Y);
                }
            } while (i < text.Length);
        }

        public static void RZSignUp(ref List<RZAccount> RZAccountsList)
        {
            Console.SetWindowSize(35, 19);
            Console.SetBufferSize(35, 19);
            RZAccount info = new RZAccount();
            do
            {
                Console.WriteLine($"Create a new user with new id: {RZAccountsList.Count}");
                do
                {
                    Console.SetCursorPosition(2, 6);
                    Console.Write("Name:    ");
                    info.RZName = Console.ReadLine();
                } while (info.RZName.Length == 0);
                do
                {
                    Console.SetCursorPosition(2, 7);
                    Console.Write("Surname: ");
                    info.RZSurname = Console.ReadLine();
                } while (info.RZSurname.Length == 0);
                do
                {
                    Console.SetCursorPosition(2, 8);
                    Console.Write("Age:     ");
                    int.TryParse(Console.ReadLine(), out int tempint);
                    info.RZAge = tempint;
                } while (info.RZAge == 0);

                Console.WriteLine($"Name:    {info.RZName}");
                Console.WriteLine($"Surname: {info.RZSurname}");
                Console.WriteLine($"Age:     {info.RZAge}");
                Console.WriteLine();

                if (info.RZName.Length > 0)
                {
                    info.RZLogin = info.RZName.ToUpper();
                    info.RZLogin += "_";
                }
                if (info.RZSurname.Length > 0)
                {
                    string temp_name = info.RZSurname.ToLower();
                    info.RZLogin += temp_name[0];
                    if (info.RZSurname.Length > 1)
                        info.RZLogin += temp_name[1];
                }
                if (info.RZAge > 0)
                {
                    if (info.RZSurname.Length > 0)
                        info.RZLogin += "_";
                    info.RZLogin += info.RZAge.ToString();
                }
                info.RZPassword = RandomString(password_len);
                //info.RZPassword = Console.ReadLine();
                for (int i = 0; i < RZAccountsList.Count; i++)
                {
                    if (info.RZLogin == RZAccountsList[i].RZLogin)
                    {
                        info.RZLogin += "c";
                    }
                }
                Console.WriteLine($"User Login:    {info.RZLogin}");
                Console.WriteLine($"User Password: {info.RZPassword}");

                Console.ReadKey();
            } while (info.RZLogin.Length == 0);
            //RZWriteBin(ref info);
            RZAccountsList.Add(info);
            //---------------------------------
            RZJson RZJsonx = new RZJson();
            RZJsonx.Save(RZAccountsList,"Users");
            //***************************************
        }
        public static void RZLogIn(ref List<RZAccount> RZAccountsList)
        {
            Console.SetBufferSize(75, 20);
            Console.SetWindowSize(75, 20);

            int u_ind = 0;

            Console.SetCursorPosition(1, 6);
            Console.WriteLine("Press Up or Down Key");
            Console.SetCursorPosition(1, 7);
            Console.Write($"User Name: {RZAccountsList[u_ind].RZLogin}");
            do
            {
                Console.SetCursorPosition(12, 7);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                int login_len = 0;
                foreach (var item in RZAccountsList)
                {
                    if (login_len < item.RZLogin.Length)
                        login_len = item.RZLogin.Length;
                }
                for (int i = 0; i <= login_len; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(12, 7);
                Console.WriteLine(RZAccountsList[u_ind].RZLogin);
                Console.ResetColor();
                Console.SetCursorPosition(12, 7);
                var cki = Console.ReadKey();
                //Console.WriteLine(cki.Key);

                if (cki.Key == ConsoleKey.Escape)
                {
                    RZMain.RZMainMenyu(ref RZAccountsList);
                }
                if (cki.Key == ConsoleKey.DownArrow)
                {
                    u_ind += 1;
                    if (u_ind >= RZAccountsList.Count)
                    {
                        u_ind = RZAccountsList.Count - 1;
                    }
                }
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    u_ind -= 1;
                    if (u_ind <= 0)
                    {
                        u_ind = 0;
                    }
                }
                if (cki.Key == ConsoleKey.Enter)
                {
                    string temp_password;
                    Console.SetCursorPosition(1, 8);
                    Console.WriteLine("User Pass: ");
                    Console.SetCursorPosition(1, 6);
                    Console.WriteLine("                    ");
                    Console.SetCursorPosition(12, 8);
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    for (int i = 0; i <= login_len; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition(12, 8);
                    temp_password = Console.ReadLine();
                    Console.ResetColor();

                    if (temp_password == RZAccountsList[u_ind].RZPassword)
                    {
                        Console.SetCursorPosition(20, 0);
                        for (int i = 0; i < password_len; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.SetCursorPosition(1, 9);
                        Console.WriteLine("                      ");
                        Console.SetCursorPosition(1, 9);
                        Console.WriteLine("Correct Password");
                        Console.SetCursorPosition(50, 0);
                        RZMain.ThisUser = RZAccountsList[u_ind].RZLogin;
                        Console.WriteLine($"Welcome, { RZMain.ThisUser }");

                        RZMain.RZMainMenyu(ref RZAccountsList);
                    }
                    else
                    {
                        Console.SetCursorPosition(1, 9);
                        Console.WriteLine("Don't correct password");
                        Console.SetCursorPosition(1, 6);
                        Console.WriteLine("Press Up or Down Key");
                        Console.SetCursorPosition(1, 8);
                        Console.WriteLine("                                  ");
                        Console.SetCursorPosition(12, 7);
                    }
                    //Console.WriteLine(oldInfo[u_ind].rz_password);
                }
                if (cki.Key == ConsoleKey.F1)
                {
                    Console.SetCursorPosition(20, 0);
                    Console.WriteLine(RZAccountsList[u_ind].RZPassword);
                }
            } while (true);

        }
        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = //"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                 //"abcdefghijklmnopqrstuvwxyz" +
                                 "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
