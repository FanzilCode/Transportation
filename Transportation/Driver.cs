using System;

namespace Transportation
{
    class Driver
    {
        public string name { get; private set; } // ФИО
        public int exp { get; private set; } // стаж (кол-во лет)

        public Driver(string name, int exp) // конструктор
        {
            this.name = name;
            this.exp = exp;
        }
        public Driver(string[] arr)
        {
            name = arr[0]; exp = Convert.ToInt32(arr[1]);
        }
        public override string ToString() // переопределение метода ToString() для сохранения в файл
        {
            return $"{name}%{exp}";
        }
        public string PrintDriver()
        {
            return $"ФИО: {name}\n" +
                $"Стаж: {exp} лет"; 
        }
    }
}
