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
            Console.WriteLine($"Название: {name}\n" +
                $"Дальность: {distance} км\n" +
                $"Кол-во дней в пути: {time}\n" +
                $"Оплата: {pay} рублей.");
        }
    }
}
