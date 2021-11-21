using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    class WorkWith1D : IWork
    {
        public Route route { get; set; } // Маршрут
        public DateTime time1 { get; set; } // дата отправки
        public DateTime time2 { get; set; } // дата возвращения
        public double reward { get; set; } // премия
        public Driver driver { get; private set; } // водитель

        public WorkWith1D(Route route, DateTime time1, DateTime time2, double reward, Driver driver)
        {
            this.route = route; this.time1 = time1; this.time2 = time2; this.reward = reward; this.driver = driver;
        }

        public WorkWith1D(Route route, DateTime time1, DateTime time2)
        {
            this.route = route; this.time1 = time1; this.time2 = time2;
        }

        public override string ToString() // переопределение метода ToString() для сохранения в файл
        {
            return $"{route}\n" +
                $"{reward}%{time1.ToShortDateString()}%{time2.ToShortDateString()}\n" +
                $"{driver}\n";
        }
        public void PrintWork() // метод для выведения информации о проделанной работе на экран
        {
            Console.WriteLine($"{route.name}\t{time1.ToShortDateString()}\t\t{time2.ToShortDateString()}\t\t{route.pay}\t\t\t{reward}\t\t{route.pay+reward}");
        }
        public void PrintWork(int k) // метод для выведения информации о проделанной работе на экран
        {
            Console.WriteLine($"{driver.name}\t\t{route.pay}\t\t\t{reward}\t\t{route.pay + reward}");
        }
        public static bool operator == (WorkWith1D work1, WorkWith1D work2) // оператор равенства
        {
            return (work1.route == work2.route) && (work1.time1 == work2.time1) && (work1.time2 == work2.time2);
        }
        public static bool operator !=(WorkWith1D work1, WorkWith1D work2)
        {
            return !(work1 == work2);
        }
        public bool IsAvailable(Driver driver, DateTime time1, DateTime time2)
        {
            return (this.driver == driver) && (this.time1 >= time1) && (this.time2 <= time2);
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
