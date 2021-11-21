using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Transportation
{
    class Menu
    {
        // список отчетов о проделанных работах
        static List<IWork> works = new List<IWork>();
        // список водителей
        static List<Driver> drivers = new List<Driver>();
        // список маршрутов
        static List<Route> routes = new List<Route>();
        // AddWork() - метод для добавления нового отчёта о проделанной работе
        public static void AddWork()
        {
            Console.WriteLine("Выберите маршрут:");
            // вызов метода PrintRoutes() - выведение на экран списка маршрутов
            PrintRoutes();
            // index1 - переменная, которая будет хранить индекс(номер в списке) выбранного маршрута
            int index1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Дата отправления: "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Дата возвращения: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Премия, в руб: "); double reward = Convert.ToDouble(Console.ReadLine());
            Console.Write("Кол-во водителей(1 или 2): "); int choise2 = Convert.ToInt32(Console.ReadLine());
            if (choise2 == 1)
            {
                Console.WriteLine("Выберите водителя(напишите индекс): ");
                // Вызов метода PrintFreeDrivers(time1, time2) - выведение на экран списка свободных водителей на указанный период времени
                PrintFreeDrivers(time1, time2);
                // переменная index2 - будет хранить индекс (номер в списке) выбранного водителя
                int index2 = Convert.ToInt32(Console.ReadLine());
                if (!(index2 < 0 || index2 > 2))
                    // добавление в список отчета о новой выполненной работе
                    works.Add(new WorkWith1D(routes[index1], time1, time2, reward, drivers[index2]));
            }
            else
            {
                Console.WriteLine("Выберите водителя 1(напишите индекс): ");
                // Вызов метода PrintFreeDrivers(time1, time2) - выведение на экран списка свободных водителей на указанный период времени
                PrintFreeDrivers(time1, time2);
                // переменная index21 - будет хранить индекс (номер в списке) первого выбранного водителя
                int index21 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Выберите водителя 2(напишите индекс): ");
                // Вызов метода PrintFreeDrivers(time1, time2) - выведение на экран списка свободных водителей на указанный период времени
                PrintFreeDrivers(time1, time2);
                // переменная index22 - будет хранить индекс (номер в списке) первого выбранного водителя
                int index22 = Convert.ToInt32(Console.ReadLine());
                if (!(index21 == index22 || (index21 > routes.Count || index21 < 0) || (index22 > routes.Count || index22 < 0)))
                    // добавление в список отчета о новой выполненной работе
                    works.Add(new WorkWith2D(routes[index1], time1, time2, reward, drivers[index21], drivers[index22]));
            }
        }
        // GetWork() - метод для получения информации о конкретной проделанной работе
        public static void GetWork()
        {
            Console.Write("Маршрут: "); string name = Console.ReadLine();
            Console.Write("Дальность: "); double distance = Convert.ToDouble(Console.ReadLine());
            Console.Write("Кол-во дней в пути: "); int time = Convert.ToInt32(Console.ReadLine());
            Console.Write("Дата отправления: "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Дата возвращения: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Оплата, в руб: "); double pay = Convert.ToDouble(Console.ReadLine());
            // создаём новый экземпляр класса Route
            Route route = new Route(name, distance, time, pay);
            Console.WriteLine("Водитель\t\t\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
            // переменная i - будет хранить в себе кол-во подходящих по описанию отчётов
            int i = 0;
            // цикл по списку отчётов о выполненных работах
            foreach (var work in works)
            {
                // если текущий отчёт подходит по описанию
                if (work.IsAvailable(route, time1, time2))
                {
                    // выводим информацию о нём на экран
                    work.PrintWork(1);
                    // увеличиваем значение переменной i на единицу
                    i++;
                }
            }
            // если нет ни одного подходящего по описанию отчёта
            if (i == 0)
                // сообщаем об этом
                Console.WriteLine("Ничего не найдено.");
        }
        // PrintWorks() - метод для печати на экран списка отчётов о проделанных работах
        public static void PrintWorks()
        {
            // если список отчётов не пуст
            if (works.Count > 0)
                Console.WriteLine("Маршрут\tДата отправления\tДата возвращения\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
            // цикл по списку отчёт о проделанных работах
            foreach (var work in works)
            {
                // вызов метода PrintWork() для выведения на кэран информации о проделанной работе
                work.PrintWork();
            }
        }
        // перегрузка метода PrintWorks() для печати на экран списка отчётов о проделанных работах в определённый период
        public static void PrintWorks(DateTime time1, DateTime time2)
        {
            // переменная i - хранит в себе кол-во отчётов о проделанных работах в период с time1 по time2
            int i = 0;
            // цикл по списку отчётов о проделанных работах
            foreach (var work in works)
            {
                // вызов перегруженного метода IWork.IsAvailable(DateTime, DateTime) для проверки принадлежности текущей работы периоду с time1 по time2
                if (work.IsAvailable(time1, time2))
                {
                    if (i == 0)
                        // если нижняя строка ещё ни разу не печаталась на экран, то сейчас она там появится
                        Console.WriteLine("Маршрут\tДата отправления\tДата возвращения\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
                    // вызов метода PrintWork() для выведения на экран отчёта о проделанной работе
                    work.PrintWork();
                    // увеличение значения переменной i на единицу
                    i++;
                }
            }
            // если нет отчётов о проделанных работах в выбанный период
            if (i == 0)
                // на экран выведется "Ничего не найдено."
                Console.WriteLine("Ничего не найдено.");
        }
        // перегрузка метода PrintWorks() для печати на экран списка отчётов о проделанных работах в определённый период выбранным водителем
        public static void PrintWorks(DateTime time1, DateTime time2, Driver driver)
        {
            // переменная i - хранит в себе кол-во отчётов о проделанных работах в период с time1 по time2 выбранным водителем
            int i = 0;
            // цикл по списку отчётов о проделанных работах
            foreach (var work in works)
            {
                // вызов перегруженного метода IWork.IsAvailable(Driver, DateTime, DateTime)
                // для проверки принадлежности текущей работы периоду с time1 по time2 водителем driver
                if (work.IsAvailable(driver, time1, time2))
                {
                    if (i == 0)
                        // если нижняя строка ещё ни разу не печаталась на экран, то сейчас она там появится
                        Console.WriteLine("Маршрут\tДата отправления\tДата возвращения\tОплата водителю, руб\tПремия,руб\tОплата с премией, руб");
                    // вызов метода PrintWork() для выведения на экран отчёта о проделанной работе
                    work.PrintWork();
                    // увеличение значения переменной i на единицу
                    i++;
                }
            }
            // если нет отчётов о проделанных работах в выбанный период определённым водителем
            if (i == 0)
                // на экран выведется "Ничего не найдено"
                Console.WriteLine("Ничего не найдено.");
        }
        // AddDriver() - метод для добавления нового водителя в список
        public static void AddDriver()
        {
            Console.Write("ФИО: ");
            // чтение с консоли ФИО водителя
            string name = Console.ReadLine();
            Console.Write("Стаж: ");
            // чтение с консоли и конвертация в тип int стажа водителя
            int exp = Convert.ToInt32(Console.ReadLine());
            // вызов метода List<>.Add() для добавления нового водителя в список
            drivers.Add(new Driver(name, exp));
            // вызов метод Clear() для очистки консоли
            Console.Clear();
            // вызов метода PrintDrivers() для просмотра списка водителей
            PrintDrivers();
        }
        // PrintDrivers() - метод для печати на экран списка водителей
        public static void PrintDrivers()
        {
            // если список не пуст
            if (drivers.Count > 0)
                // печатается следующее
                Console.WriteLine("   Фамилия Имя Отчество\t\tСтаж");
            // цикл по списку водителей
            foreach (var driver in drivers)
            {
                // на экран печается индекс(номер в списке) текущего водителя
                Console.Write(drivers.IndexOf(driver) + ") ");
                // вызов метода PrintDriver() для выведения на экран информации о водителе
                driver.PrintDriver();
            }
        }
        // PrintFreeDrivers() - метод для печати на экран списка свободных на выбранный период времени водителей
        public static void PrintFreeDrivers(DateTime time1, DateTime time2)
        {
            Console.WriteLine("Список свободных водителей:");
            Console.WriteLine("   Фамилия Имя Отчество\t\tСтаж");
            // цикл по списку водителей
            foreach (var driver in drivers)
            {
                // переменная k - будет хранить информацию о том, занят ли текущий водитель в выбранный период времени
                bool k = false;
                // цикл по списку отчётов о выполненых работах
                foreach (var work in works)
                {
                    // вызов метода IWork.IsAvailable(Driver, DateTime, DateTime) для проверки занятости водителя driver
                    // в периол времени с time1 по time2
                    if (work.IsAvailable(driver, time1, time2))
                    {
                        k = true;
                        // break - выход из цикла
                        break;
                    }
                }
                if (!k)
                {
                    // на экран печатается индекс(номер в списке) текущего водителя
                    Console.Write(drivers.IndexOf(driver) + ") ");
                    // вызов метода PrintDriver() для выведения на экран информации о водителе
                    driver.PrintDriver();
                }
            }
        }
        // PrintFreeDrivers() - метод для печати на экран списка занятых на сегодня водителей
        public static void PrintBusyDrivers()
        {
            Console.WriteLine("Список занятых водителей:");
            Console.WriteLine("   Фамилия Имя Отчество\t\tСтаж");
            // цикл по списку водителей
            foreach (var driver in drivers)
            {
                // переменная k - будет хранить информацию о том, занят ли текущий водитель в выбранный период времени
                bool k = false;
                // цикл по списку отчётов о проделанных работах
                foreach (var work in works)
                {
                    // вызов метода IWork.IsAvailable(Driver, DateTime, DateTime) для проверки занятости водителя driver
                    // на сегодняшний день
                    if (work.IsAvailable(driver, DateTime.Today, DateTime.Today))
                    {
                        k = true;
                        // break - выход из цикла
                        break;
                    }
                }
                if (k)
                {
                    // на экран печатается индекс(номер в списке) текущего водителя
                    Console.Write(drivers.IndexOf(driver) + ") ");
                    // вызов метода PrintDriver() для выведения на экран информации о водителе
                    driver.PrintDriver();
                }
            }
        }
        // AddRoute() - метод для добавления нового маршрута
        public static void AddRoute()
        {
            Console.Write("Название: ");
            // чтение названия маршрута с консоли
            string name = Console.ReadLine();
            Console.Write("Дальность, в км: ");
            // чтение с консоли и конвертация в тип double значения дистанции с консоли
            double distance = Convert.ToDouble(Console.ReadLine());
            Console.Write("Кол-во дней в пути: ");
            // чтение с консоли и конвертация в тип int с консоли
            int time = Convert.ToInt32(Console.ReadLine());
            Console.Write("Оплата водителю, руб: ");
            // чтение с консоли и конвертация в тип double с консоли
            double pay = Convert.ToDouble(Console.ReadLine());
            // вызов метода List<>.Add() для добавления в список нового элемента и вызов конструктора класса Route
            routes.Add(new Route(name, distance, time, pay));
            // вызов метода Clear() для очистки консоли
            Console.Clear();
            // вызов метода PrintRoutes() для печати на экран обновлённого списка маршрутов
            PrintRoutes();
        }
        // PrintRoutes() - метод для печати на экран списка маршрутов
        public static void PrintRoutes()
        {
            // если список не пуст
            if (routes.Count > 0)
                // на экран выведется следующая строка
                Console.WriteLine("   Название\tДальность\tКол-во дней\tОплата водителю, руб");
            // цикл по списку маршрутов
            foreach (var route in routes)
            {
                // вывод на экран индекса(номера в списке) текущего маршрута
                Console.Write(routes.IndexOf(route) + ") ");
                // вызов метода PrintRoute() для выведения на экран информации о маршруте
                route.PrintRoute();
            }
        }
        // SaveToFile() - метод для сохранения данных в файл
        public static void SaveToFile(string path)
        {
            // переменная strings будет хранить в себе информацию о проделанных работах для дальнейшего сохранения в файл
            string strings = "";
            // цикл по списку отчётов о проделанных работах
            foreach (var work in works)
            {
                if (work is WorkWith1D)
                {
                    // если текущая работа выполнена одним водителем, то сначала пропечатается "1", а затем уже информация
                    // о текущей проделанной работе
                    strings += "1" + "\n" + work;
                }
                else
                {
                    // если текущая работа выполнена двумя водителями, то сначала пропечатается "2", а затем уже информация
                    // о текущей проделанной работе
                    strings += "2" + "\n" + work;
                }
            }
            // вызов метода Trim() для удаления всех начальных и конченых пробелов из текущей строки
            strings = strings.Trim();
            // открываем файл path
            using (StreamWriter sw = new StreamWriter(path))
            {
                // и записываем в него strings
                sw.Write(strings);
            }
        }
        // ReadOnFile() - метод для чтения из файла
        public static void ReadOnFile(string path)
        {
            string line;
            // открываем файл path для чтения
            using (StreamReader sr = new StreamReader(path))
            {
                // цикл по строкам файла path
                while ((line = sr.ReadLine()) != null)
                {
                    // первая строка отчёта хранит одну цифру (1 или 2) - кол-во водителей в текущей перевозке
                    int choise = Convert.ToInt32(line);
                    // читаем следующую строку
                    line = sr.ReadLine();
                    // arr - массив строк. метод Split("%") для разбиения строки разделителем "%"
                    string[] arr = line.Split("%");
                    // создание нового экземепляра route класса Route
                    Route route = new Route(arr);
                    // переменная count будет хранить кол-во маршрутов в списке, удовлетворяющих условию равенства
                    int count = 0;
                    foreach (var r in routes)
                    {
                        if (r == route)
                        { count++; break; }
                    }
                    // если в списке нет совпадающих маршрутов
                    if (count == 0)
                        // маршрут route добавляется в список
                        routes.Add(route);
                    // теперь переменная count будет хранить кол-во водителей из списка, удовлетворящих условию равенства
                    // с выбранным водителем
                    count = 0;
                    // arr - массив строк. метод Split("%") для разбиения строки разделителем "%"
                    arr = sr.ReadLine().Split("%");
                    // конвертации из массива строк в нужный тип
                    DateTime time1 = Convert.ToDateTime(arr[1]);
                    DateTime time2 = Convert.ToDateTime(arr[2]);
                    double reward = Convert.ToDouble(arr[0]);
                    // arr - массив строк. метод Split("%") для разбиения строки разделителем "%"
                    arr = sr.ReadLine().Split("%");
                    // вызов констуктора класса Driver
                    Driver driver = new Driver(arr);
                    // цикл по списку водителей для проверки того, что в списке нет водителей с таким же ФИО и стажем
                    foreach (var d in drivers)
                    {
                        // если водитель driver и водитель из списка удовлетворяют условию равенства
                        if (driver == d)
                        {
                            // значение переменной count увеличивается на единицу
                            count++;
                            // break - выход из цикла
                            break;
                        }
                    }
                    // если совпадающих в списке нет
                    if (count == 0)
                        // водитель driver добавляется в список
                        drivers.Add(driver);
                    count = 0;
                    if (choise == 1)
                    {
                        works.Add(new WorkWith1D(route, time1, time2, reward, driver));
                    }
                    else
                    {
                        arr = sr.ReadLine().Split("%");
                        Driver driver2 = new Driver(arr);
                        foreach (var d in drivers)
                        {
                            if (driver == d) 
                                count++;
                        }
                        if (count == 0)
                            drivers.Add(driver2);
                        works.Add(new WorkWith2D(route, time1, time2, reward, driver, driver2));
                    }
                    // arr = sr.ReadLine().Split();
                }
            }
        }
        // PrintPayOfTheTime() - метод для вычисления и печати зарплаты всех водителей за определенный период
        public static bool PrintPayOfTheTime()
        {
            Console.Write("Введите дату с которой надо начать отсчет:  "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Введите дату до которой нужно вести отсчет: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine($"\t\tЗарплата за период c {time1.ToShortDateString()} по {time2.ToShortDateString()}");
            // создаём список 
            List<Pay> pays = new List<Pay>();
            // цикл по списку отчетов о проделанных работах
            foreach (var work in works)
            {
                // если текущая работа была выполнена в период с time1 по time2
                if (work.time1 >= time1 && work.time2 <= time2)
                {
                    // если текущую работу выполнил 1 водитель
                    if (work is WorkWith1D)
                    {
                        // создаем новый экземпляр класса
                        WorkWith1D work1 = (WorkWith1D)work;
                        // в список добавляется новый экземпляр
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
            // если список pays пуст функция вернёт значение false
            if (pays.Count == 0)
                return false;
            Console.WriteLine("Фамилия Имя Отчество\tОплата за маршрут, руб\tПремия,руб\tОплата с премией,руб");
            // цикл по списку pays, по окончании которого совпадающие по полю name элементы будут объеденены
            for (int i = 0; i < pays.Count; i++)
            {
                // вложенный цикл по списку pays
                for (int j = i; j < pays.Count; j++)
                {
                    if (pays[i].name == pays[j].name && i != j)
                    {
                        pays[i].Add(pays[j].routePay, pays[j].reward);
                        // вызов метода RemoveAt(j) - удаление элемента под индексом j из списка pays
                        pays.RemoveAt(j);
                        // уменьшение значения переменной j на еденицу
                        j--;
                    }
                }
            }
            // цикл по списку pay
            foreach (var pay in pays)
                // вызов метода PrintPay() для печати на экран информации о зарплате водителя
                pay.PrintPay();
            return true;
        }

        static void Main(string[] args)
        {
            string path = @"Works.txt";
            // вызов метода ReadOnFile() - чтение из файла
            ReadOnFile(path);
            int choise = 1;
            while (choise >= 1 && choise <= 3)
            {
                Console.WriteLine("\t\tГлавная\nВыберите:");
                Console.WriteLine("1) Водители\t2) Маршруты\t3) Проделанные работы\t4) Выход");
                choise = Convert.ToInt32(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        {
                            Console.WriteLine("\t\tВодители\nВыберите:");
                            Console.WriteLine("1) Получить список свободных водителей\n2) Получить список занятых водителей");
                            Console.WriteLine("3) Получить список всех водителей\n4) Добавить водителя");
                            Console.WriteLine("5) Получить информацию о работе водителя за период");
                            Console.WriteLine("6) Получить информацию о зарплате водителей за период\n7) Вернуться на главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            switch (choise2)
                            {
                                case 1:
                                    PrintFreeDrivers(DateTime.Today, DateTime.Today);
                                    break;
                                case 2:
                                    PrintBusyDrivers();
                                    break;
                                case 3:
                                    PrintDrivers();
                                    break;
                                case 4:
                                    AddDriver();
                                    break;
                                case 5:
                                    Console.WriteLine("\tРабота водителя за период");
                                    Console.Write("Введите дату с которой надо начать отсчет:  "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
                                    Console.Write("Введите дату до которой нужно вести отсчет: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());
                                    Console.WriteLine("Выберите водителя(введите индекс):"); PrintDrivers();
                                    int k = Convert.ToInt32(Console.ReadLine());
                                    PrintWorks(time1, time2, drivers[k]);
                                    break;
                                case 6:
                                    PrintPayOfTheTime();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("\t\tМаршруты\nВыберите:\n1) Получить список маршрутов\n2) Добавить маршрут\n3) Вернуться на главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            switch (choise2)
                            {
                                case 1:
                                    PrintRoutes();
                                    break;
                                case 2:
                                    AddRoute();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("\t\tПроделанные работы\nВыберите:\n1) Получить список всех проделанных работ");
                            Console.WriteLine("2) Получить информацию по конкретной проделанной работе");
                            Console.WriteLine("3) Получить список работ, проделанных за определённый период");
                            Console.WriteLine("4) Получить информацию о проделанной за сегодня работе");
                            Console.WriteLine("5) Добавить отчёт о проделанной работе\n6) Вернуться на главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            switch (choise2)
                            {
                                case 1:
                                    PrintWorks();
                                    break;
                                case 2:
                                    GetWork();
                                    break;
                                case 3:
                                    Console.Write("Введите дату с которой надо начать отсчет:  "); DateTime time1 = Convert.ToDateTime(Console.ReadLine());
                                    Console.Write("Введите дату до которой нужно вести отсчет: "); DateTime time2 = Convert.ToDateTime(Console.ReadLine());
                                    PrintWorks(time1, time2);
                                    break;
                                case 4:
                                    Console.WriteLine("\t\t\tРабота за день");
                                    PrintWorks(DateTime.Today, DateTime.Today);
                                    break;
                                case 5:
                                    AddWork();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            SaveToFile(path);
            Console.ReadKey();
        }
    }
}