using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    class WorkWith2D : IWork
    {
        public Route route { get; set; } // Маршрут
        public DateTime time1 { get; set; } // дата отправки
        public DateTime time2 { get; set; } // дата возвращения
        public double reward { get; set; } // премия
        public Driver driver1 { get; private set; } // первый водитель
        public Driver driver2 { get; private set; } // второй водитель

        public WorkWith2D(Route route, DateTime time1, DateTime time2, double reward, Driver driver1, Driver driver2) // конструктор
        {
            this.route = route; this.time1 = time1; this.time2 = time2; this.reward = reward; this.driver1 = driver1; this.driver2 = driver2;
        }

        public WorkWith2D(Route route, DateTime time1, DateTime time2) // конструктор без учета водителей и премии
        {
            this.route = route; this.time1 = time1; this.time2 = time2;
        }

        public override string ToString() // переопределение метода ToString для записи в файл
        {
            return $"{route}\n" +
                $"{reward}%{time1.ToShortDateString()}%{time2.ToShortDateString()}\n" +
                $"{driver1}\n" +
                $"{driver2}\n";
        }
        public void PrintWork() // метод для выведения на экран информации о проделанной работе
        {
            Console.WriteLine($"{route.name}\t{time1.ToShortDateString()}\t\t{time2.ToShortDateString()}\t\t{(double)route.pay/2}\t\t\t{reward}\t\t{(double)route.pay/2 + reward}");
        }
        public void PrintWork(int k) // метод для выведения на экран информации о проделанной работе
        {
            Console.WriteLine($"{driver1.name}\t\t{(double)route.pay / 2}\t\t\t{reward}\t\t{(double)route.pay / 2 + reward}");
            Console.WriteLine($"{driver2.name}\t\t{(double)route.pay / 2}\t\t\t{reward}\t\t{(double)route.pay / 2 + reward}");
        }
        public static bool operator ==(WorkWith2D work1, WorkWith2D work2) // оператор равенства
        {
            return (work1.route == work2.route) && (work1.time1 == work2.time1) && (work1.time2 == work2.time2);
        }
        public static bool operator !=(WorkWith2D work1, WorkWith2D work2)
        {
            return !(work1 == work2);
        }
        public bool IsAvailable(Driver driver, DateTime time1, DateTime time2)
        {
            return (((this.driver1 == driver) || this.driver2 == driver) && (this.time1 >= time1) && (this.time2 <= time2));
        }
        public bool IsAvailable(Route route, DateTime time1, DateTime time2)
        {
            return (this.route == route) && (this.time1 == time1) && (this.time2 == time2);
        }
        public bool IsAvailable(DateTime time1, DateTime time2)
        {
            return (this.time1 >= time1 && this.time2 <= time2);
        }
    }
}
