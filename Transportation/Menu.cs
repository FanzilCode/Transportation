using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Transportation
{
    class Menu
    {
        static List<IWork> works = new List<IWork>();
        static List<Driver> drivers = new List<Driver>();
        static List<Route> routes = new List<Route>();
        public static bool PrintWorks(bool k = true)
        {
            if (k)
            {
                if(works.Count > 0)
                    Console.WriteLine("Маршрут\tДата отправления\tДата возвращения\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
                foreach (var work in works)
                {
                    work.PrintWork();
                }
                Console.WriteLine("Вы хотите добавить запись о новой проделанной работе?\nВыберите: 1) Да\t2) Нет");
                int choise = Convert.ToInt32(Console.ReadLine());
                if (choise == 1)
                {
                    Console.WriteLine("Выберите маршрут:");
                    PrintRoutes();
                    int index1 = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Дата отправления: "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Дата возвращения: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Премия, в руб: "); double reward = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Кол-во водителей(1 или 2): "); int choise2 = Convert.ToInt32(Console.ReadLine());
                    if (choise2 == 1)
                    {
                        Console.WriteLine("Выберите водителя(напишите индекс): ");
                        PrintDrivers();
                        int index2 = Convert.ToInt32(Console.ReadLine());
                        if (index2 < 0 || index2 > 2)
                            return false;
                        works.Add(new WorkWith1D(routes[index1], time1, time2, reward, drivers[index2]));
                    }
                    else
                    {
                        Console.WriteLine("Выберите водителя 1(напишите индекс): ");
                        PrintDrivers();
                        int index21 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Выберите водителя 2(напишите индекс): ");
                        PrintDrivers();
                        int index22 = Convert.ToInt32(Console.ReadLine());
                        if (index21 == index22 || (index21 > routes.Count || index21 < 0) || (index22 > routes.Count || index22 < 0))
                            return false;
                        works.Add(new WorkWith2D(routes[index1], time1, time2, reward, drivers[index21], drivers[index22]));
                    }
                }
            }
            else
            {
                Console.Write("Маршрут: "); string name = Console.ReadLine();
                Console.Write("Дальность: "); double distance = Convert.ToDouble(Console.ReadLine());
                Console.Write("Кол-во дней в пути: "); int time = Convert.ToInt32(Console.ReadLine());
                Console.Write("Дата отправления: "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Дата возвращения: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Оплата, в руб: "); double pay = Convert.ToDouble(Console.ReadLine());
                Route route = new Route(name, distance, time, pay);
                Console.WriteLine("Водитель\t\t\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
                int i = 0;
                foreach (var work in works)
                {
                    if(work.IsAvailable(route, time1, time2))
                    {
                        work.PrintWork(1); i++;
                    }
                }
                if (i == 0) Console.WriteLine("Ничего не найдено.");
            }
            return true;
        }
        public static void PrintDrivers(bool k = true)
        {
            if(drivers.Count > 0)
                Console.WriteLine("  Фамилия Имя Отчество\t\tСтаж");
            foreach (var driver in drivers)
            {
                Console.Write(drivers.IndexOf(driver) + ") ");
                driver.PrintDriver();
            }
            if(!k)
            {
                Console.WriteLine("Вы хотите добавить нового водителя?\nВыберите: 1) Да\t2) Нет");
                int choise = Convert.ToInt32(Console.ReadLine());
                if(choise == 1)
                {
                    Console.Write("ФИО: "); string name = Console.ReadLine();
                    Console.Write("Стаж: "); int exp = Convert.ToInt32(Console.ReadLine());
                    drivers.Add(new Driver(name, exp));
                    Console.Clear(); 
                    PrintDrivers();
                }
            }
        }
        public static void PrintRoutes(bool k = true)
        {
            if(routes.Count > 0)
                Console.WriteLine("  Название\tДальность\tКол-во дней\tОплата водителю, руб");
            foreach (var route in routes)
            {
                Console.Write(routes.IndexOf(route) + ") ");
                route.PrintRoute();
            }
            if(!k)
            {
                Console.WriteLine("Вы хотите добавить новый маршрут?\nВыберите: 1) Да\t 2) Нет");
                int choise = Convert.ToInt32(Console.ReadLine());
                if (choise == 1)
                {
                    Console.Write("Название: "); string name = Console.ReadLine();
                    Console.Write("Дальность, в км: "); double distance = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Кол-во дней в пути: "); int time = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Оплата водителю, руб: "); double pay = Convert.ToDouble(Console.ReadLine());
                    routes.Add(new Route(name, distance, time, pay));
                    Console.Clear();
                    PrintRoutes();
                }
            }
        }
        public static void SaveToFile(string path) // сохранение в файл
        {
            string strings = "";
            foreach (var work in works)
            {
                if (work is WorkWith1D)
                {
                    strings += "1" + "\n" + work;
                }
                else
                {
                    strings += "2" + "\n" + work;
                }
            }
            strings = strings.Trim();

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(strings);
            }
        }
        public static void ReadOnFile(string path)
        {
            string line;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    int choise = Convert.ToInt32(line);
                    line = sr.ReadLine();
                    string[] arr = line.Split("%");
                    IWork work;
                    Route route = new Route(arr);
                    int count = 0;
                    foreach (var r in routes)
                    {
                        if (r == route) count++;
                    }
                    if(count == 0)
                        routes.Add(route);
                    count = 0;
                    arr = sr.ReadLine().Split("%");
                    DateTime time1 = Convert.ToDateTime(arr[1]);
                    DateTime time2 = Convert.ToDateTime(arr[2]);
                    double reward = Convert.ToDouble(arr[0]);
                    arr = sr.ReadLine().Split("%");
                    Driver driver = new Driver(arr);
                    foreach(var d in drivers)
                    {
                        if (driver == d) count++;
                    }
                    if(count == 0)
                        drivers.Add(driver);
                    if(choise == 1)
                    {
                        works.Add(new WorkWith1D(route, time1, time2, reward, driver));
                    }
                    else
                    {
                        arr = sr.ReadLine().Split("%");
                        Driver driver2 = new Driver(arr);
                        drivers.Add(driver2);
                        works.Add(new WorkWith2D(route, time1, time2, reward, driver, driver2));
                    }
                    // arr = sr.ReadLine().Split();
                }
            }
        }
        public static void PrintWorkOfDriver()
        {
            Console.WriteLine("\tРабота водителя за период");
            Console.Write("Введите дату с которой надо начать отсчет:  "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Введите дату до которой нужно вести отсчет: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Введите ФИО водителя:"); string name = Console.ReadLine();
            Console.Write("Введите стаж водителя:"); int exp = Convert.ToInt32(Console.ReadLine());
            Driver driver = new Driver(name, exp); int i = 0;
            foreach (var work in works)
            {
                if (work.IsAvailable(driver, time1, time2))
                {
                    if (i == 0)
                        Console.WriteLine("Маршрут\tДата отправления\tДата возвращения\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
                    work.PrintWork(); i++;
                }
            }
            if (i == 0) Console.WriteLine("Ничего не найдено.");
        }
        public static void PrintWorkOfToday()
        {
            int i = 0;
            Console.WriteLine("\t\t\tРабота за день");
            Console.WriteLine("Маршрут\tДата отправления\tДата возвращения\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
            foreach (var work in works)
            {
                if (work.time1 == work.time2 && work.time1 == DateTime.Today) { work.PrintWork(); i++; }
            }
            if (i == 0) Console.WriteLine("Ничего не найдено.");
        }
        public static bool PrintPayOfTheTime()
        {
            Console.Write("Введите дату с которой надо начать отсчет:  "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Введите дату до которой нужно вести отсчет: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine($"\t\tЗарплата за период c {time1.ToShortDateString()} по {time2.ToShortDateString()}");
            List<Pay> pays = new List<Pay>();
            foreach (var work in works)
            {
                if (work.time1 >= time1 && work.time2 <= time2)
                {
                    if (work is WorkWith1D)
                    {
                        WorkWith1D work1 = (WorkWith1D)work;
                        pays.Add(new Pay(work1.driver.name, work1.route.pay, work1.reward));
                    }
                    else
                    {
                        WorkWith2D work1 = (WorkWith2D)work;
                        pays.Add(new Pay(work1.driver1.name, (double)work1.route.pay / 2, work1.reward));
                        pays.Add(new Pay(work1.driver2.name, (double)work1.route.pay / 2, work1.reward));
                    }
                }
            }
            if (pays.Count == 0)
                return false;
            Console.WriteLine("Фамилия Имя Отчество\tОплата за маршрут, руб\tПремия,руб\tОплата с премией,руб");
            
            for (int i = 0; i < pays.Count; i++)
            {
                for (int j = i; j < pays.Count; j++)
                {
                    if (pays[i].name == pays[j].name && i != j)
                    {
                        pays[i].Add(pays[j].routePay, pays[j].reward);
                        pays.RemoveAt(j); j--;
                    }
                }
            }
            foreach (var pay in pays)
                pay.PrintPay();
            return true;
        }

        static void Main(string[] args)
        {
            string path = @"Works.txt";
            ReadOnFile(path);
            int choise = 1;
            while (choise >= 1 && choise <= 5)
            {
                Console.WriteLine("\tГлавная\nВыберите:");
                Console.WriteLine("1)Формы\n2)Водители\n3)Зарплата за период\n4)Работа водителя за период\n5)Работа за день\n6)Выйти из приложения");
                choise = Convert.ToInt32(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("Выберите:\n1)Работа-список\n2)Работа(получить информацию о конкретной работе)\n3)Маршруты\n4)Водители\nИначе: Выход");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            switch (choise2)
                            {
                                case 1:
                                    PrintWorks(); break;
                                case 2:
                                    PrintWorks(false); break;
                                case 3:
                                    PrintRoutes(false); break;
                                case 4:
                                    PrintDrivers(false); break;
                                default:
                                    break;
                            }
                            break;
                        }
                    case 2:
                        PrintDrivers(); break;
                    case 3:
                        PrintPayOfTheTime(); break;
                    case 4:
                        PrintWorkOfDriver(); break;
                    case 5:
                        PrintWorkOfToday(); break;
                    default:
                        break;
                }
            }
            SaveToFile(path);
            Console.ReadKey();
        }
    }
}
