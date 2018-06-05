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
    class RZTasks
    {
        [DataMember]
        public string RZTTitle { get; set; }
        [DataMember]
        public bool RZTDone { get; set; }
        [DataMember]
        public string RZTPriority { get; set; }
        [DataMember]
        private DateTime rzTDateCreate;
        public DateTime RZTDateCreate
        {
            get { return rzTDateCreate; }
            set { rzTDateCreate = DateTime.Now; }
        }
        [DataMember]
        public DateTime RZTDateEnd { get; set; }
        [DataMember]
        private string rzTUser;
        public string RZTUser
        {
            get { return rzTUser; }
            set { rzTUser = RZMain.ThisUser; }
        }

        public static void RZAddTask (ref List<RZTasks> RZTasksList)
        {
            Console.WriteLine();
            RZTasks tempTask = new RZTasks();

            Console.Write("Insert Title: ");
            tempTask.RZTTitle = Console.ReadLine();

            Console.Write("Choose Done task: ");
            tempTask.RZTDone = RZDoneParamChange(17, 8);

            Console.Write("Choose Priority task: ");
            tempTask.RZTPriority = RZPriorityParamChange(21, 9);

            tempTask.RZTDateCreate = DateTime.Now;

            Console.Write("Insert how much day active: ");
            double.TryParse(Console.ReadLine(), out double tempint);
            tempTask.RZTDateEnd = DateTime.Now;
            tempTask.RZTDateEnd = tempTask.RZTDateEnd.AddDays(tempint);

            tempTask.RZTUser = RZMain.ThisUser;

            RZTasksList.Add(tempTask);
            RZJson RZSaveJson = new RZJson();
            RZSaveJson.Save(RZTasksList, "Tasks");
        }

        public static bool RZDoneParamChange(int left, int top)
        {
            ConsoleKeyInfo cki;
            int m_ind = 0;
            int m_count = 2;
            var m_list = new string[m_count];
            m_list[0] = " No  ";
            m_list[1] = " Yes ";
            Console.SetCursorPosition(left, top);
            Console.WriteLine(m_list[0]);
            do
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine(m_list[m_ind]);

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
                        case 0:
                            return true;
                        case 1:
                            return false;
                    }
                }
            } while (true);
        }

        public static string RZPriorityParamChange(int left, int top)
        {
            ConsoleKeyInfo cki;
            int m_ind = 1;
            int m_count = 3;
            var m_list = new string[m_count];
            m_list[0] = "  High  ";
            m_list[1] = " Normal ";
            m_list[2] = "  Low   ";

            Console.SetCursorPosition(left, top);
            Console.WriteLine(m_list[0]);
            do
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine(m_list[m_ind]);

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
                        case 0:
                            return "High";
                        case 1:
                            return "Normal";
                        case 2:
                            return "Low";
                    }
                }
            } while (true);
        }

        public override string ToString()
        {
            return $"\n{rzTUser}\n{RZTDateCreate} - {(RZTDone ? "Active" : "Not Active" )} - {RZTPriority} - {RZTDateEnd}\n" +
                   $"---------------------------------------------------------\n{RZTTitle}\n" +
                   $"---------------------------------------------------------";
        }
    }
}
