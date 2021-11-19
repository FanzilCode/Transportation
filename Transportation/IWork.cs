using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    interface IWork
    {
        Route route { get; set; } // Маршрут
        DateTime dateOfDispatch { get; set; } // дата оправления
        DateTime dateOfReturn { get; set; } // дата возвращения
        double reward { get; set; } // премия
    }
}
