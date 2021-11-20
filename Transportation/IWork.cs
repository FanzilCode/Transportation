using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    interface IWork
    {
        Route route { get; set; } // Маршрут
        DateTime time1 { get; set; } // дата оправления
        DateTime time2 { get; set; } // дата возвращения
        double reward { get; set; } // премия

        public void PrintWork();
        public bool IsAvailable(Driver driver, DateTime time1, DateTime time2);
        public bool IsAvailable(Route route, DateTime time1, DateTime time2);
        public void PrintWork(int k);
    }
}
