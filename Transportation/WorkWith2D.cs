using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    class WorkWith2D : IWork
    {
        public Route route { get; set; } // Маршрут
        public DateTime dateOfDispatch { get; set; } // дата отправки
        public DateTime dateOfReturn { get; set; } // дата возвращения
        public double reward { get; set; } // премия
        public Driver driver1 { get; private set; } // первый водитель
        public Driver driver2 { get; private set; } // второй водитель

        public override string ToString()
        {
            return $"{route}\n" +
                $"{dateOfDispatch} {dateOfReturn}\n" +
                $"{reward}\n" +
                $"{driver1}\n" +
                $"{driver2}\n";
        }
    }
}
