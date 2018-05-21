using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RZToDoListApp
{
    class RZAccount
    {
        public string RZName { get; set; }
        public string RZSurname { get; set; }
        public int RZAge { get; set; }
        public string RZLogin { get; set; }
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

        public static void RZSignUp(ref RZAccount[] oldInfo, int userCount)
        {
            Console.SetWindowSize(35, 19);
            Console.SetBufferSize(35, 19);
            var info = new RZAccount[userCount];
            if (userCount > 1)
            {
                for (int i = 0; i < oldInfo.Length; i++)
                {
                    info[i].rz_name = oldInfo[i].rz_name;
                    info[i].rz_surname = oldInfo[i].rz_surname;
                    info[i].rz_age = oldInfo[i].rz_age;
                    info[i].rz_login = oldInfo[i].rz_login;
                    info[i].rz_password = oldInfo[i].rz_password;
                }
            }

            userCount -= 1;
            do
            {
                Console.WriteLine(userCount);
                do
                {
                    Console.SetCursorPosition(2, 6);
                    Console.Write("Name:    ");
                    info[userCount].rz_name = Console.ReadLine();
                } while (info[userCount].rz_name.Length == 0);
                do
                {
                    Console.SetCursorPosition(2, 7);
                    Console.Write("Surname: ");
                    info[userCount].rz_surname = Console.ReadLine();
                } while (info[userCount].rz_surname.Length == 0);
                do
                {
                    Console.SetCursorPosition(2, 8);
                    Console.Write("Age:     ");
                    int.TryParse(Console.ReadLine(), out info[userCount].rz_age);
                } while (info[userCount].rz_age == 0);

                Console.WriteLine($"Name:    {info[userCount].rz_name}");
                Console.WriteLine($"Surname: {info[userCount].rz_surname}");
                Console.WriteLine($"Age:     {info[userCount].rz_age}");
                Console.WriteLine();

                if (info[userCount].rz_name.Length > 0)
                {
                    info[userCount].rz_login = info[userCount].rz_name.ToUpper();
                    info[userCount].rz_login += "_";
                }
                if (info[userCount].rz_surname.Length > 0)
                {
                    string temp_name = info[userCount].rz_surname.ToLower();
                    info[userCount].rz_login += temp_name[0];
                    if (info[userCount].rz_surname.Length > 1)
                        info[userCount].rz_login += temp_name[1];
                }
                if (info[userCount].rz_age > 0)
                {
                    if (info[userCount].rz_surname.Length > 0)
                        info[userCount].rz_login += "_";
                    info[userCount].rz_login += info[userCount].rz_age.ToString();
                }

                info[userCount].rz_password = RandomString(password_len);

                Console.WriteLine($"User Login:    {info[userCount].rz_login}");
                Console.WriteLine($"User Password: {info[userCount].rz_password}");

                Console.ReadKey();

            } while (info[userCount].rz_login.Length == 0);
            RZWriteBin(ref info);
            oldInfo = info;
        }
        public static void RZLogIn(ref RZPersonalInfo[] oldInfo)
        {
            if (oldInfo.Length == 0)
            {
                Console.WriteLine("Users is not exist!");
                Console.ReadKey();
                return;
            }
            Console.SetBufferSize(75, 20);
            Console.SetWindowSize(75, 20);

            int u_ind = 0;

            Console.SetCursorPosition(1, 6);
            Console.WriteLine("Press Up or Down Key");
            Console.SetCursorPosition(1, 7);
            Console.Write($"User Name: {oldInfo[u_ind].rz_login}");
            do
            {
                Console.SetCursorPosition(12, 7);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                int login_len = 0;
                foreach (var item in oldInfo)
                {
                    if (login_len < item.rz_login.Length)
                        login_len = item.rz_login.Length;
                }
                for (int i = 0; i <= login_len; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(12, 7);
                Console.WriteLine(oldInfo[u_ind].rz_login);
                Console.ResetColor();
                Console.SetCursorPosition(12, 7);
                var cki = Console.ReadKey();
                //                Console.WriteLine(cki.Key);
                if (cki.Key == ConsoleKey.Escape)
                {
                    RZMenyu(ref oldInfo);
                }
                if (cki.Key == ConsoleKey.DownArrow)
                {
                    u_ind += 1;
                    if (u_ind >= oldInfo.Length)
                    {
                        u_ind = oldInfo.Length - 1;
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

                    if (temp_password == oldInfo[u_ind].rz_password)
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
                        Console.WriteLine($"Welcome, { oldInfo[u_ind].rz_name }");
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


                    //                    Console.WriteLine(oldInfo[u_ind].rz_password);
                }
                if (cki.Key == ConsoleKey.F1)
                {
                    Console.SetCursorPosition(20, 0);
                    Console.WriteLine(oldInfo[u_ind].rz_password);
                }
            } while (true);

        }
    }
}
