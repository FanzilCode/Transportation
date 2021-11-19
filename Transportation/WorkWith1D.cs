using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    class WorkWith1D : IWork
    {
        public Route route { get; set; } // Маршрут
        public DateTime dateOfDispatch { get; set; } // дата отправки
        public DateTime dateOfReturn { get; set; } // дата возвращения
        public double reward { get; set; } // премия
        public Driver driver { get; private set; } // водитель

        public override string ToString() // переопределение метода ToString() для сохранения в файл
        {
            return $"{route}%{reward}%{dateOfDispatch}%{dateOfReturn}\n" +
                $"{driver}\n";
        }
        public void PrintWork()
        {
            Console.WriteLine("");
        }
    }
}
