using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    class Route
    {
        public string name { get; private set; } // название
        public double distance { get; private set; } // дальность (в км)
        public int time { get; private set; } // кол-во дней в пути
        public double pay { get; private set; } // оплата (в рублях)
        public Route(string name, double distance, int time, double pay) // конструктор
        {
            this.name = name;
            this.distance = distance;
            this.time = time;
            this.pay = pay;
        }
        public Route(string[] arr) // конструктор для чтения из файла
        {
            name = arr[0]; distance = Convert.ToDouble(arr[1]);
            time = Convert.ToInt32(arr[2]); pay = Convert.ToDouble(arr[3]);
        }
        public override string ToString() // переопределяем метод ToString() для сохранения в файл
        {
            return $"{name}%{distance}%{time}%{pay}";
        }
        public void PrintRoute()
        {
            Console.WriteLine($"{name}\t{distance}\t\t{time}\t\t{pay}");
        }
        public static bool operator == (Route route1, Route route2)
        {
            return (route1.name == route2.name) && (route1.time == route2.time) && (route1.distance == route2.distance);
        }
        public static bool operator !=(Route route1, Route route2)
        {
            return !(route1 == route2);
        }
    }
}
