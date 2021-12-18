using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    abstract class Work : IWork
    {
        public Route route { get; set; } // Маршрут
        public DateTime time1 { get; set; } // дата отправки
        public DateTime time2 { get; set; } // дата возвращения
        public double reward { get; set; } // премия
        public bool IsAvailable(Route route, DateTime time1, DateTime time2)
        {
            return (this.route == route) && (this.time1 == time1) && (this.time2 == time2);
        }
        public bool IsAvailable(DateTime time1, DateTime time2)
        {
            return (this.time1 >= time1 && this.time2 <= time2);
        }
        public abstract void PrintWork(); // метод для выведения информации о проделанной работе на экран
        public abstract bool IsAvailable(Driver driver, DateTime time1, DateTime time2);
        public abstract void PrintWork(int k); // метод для выведения информации о проделанной работе на экран
    }
}
