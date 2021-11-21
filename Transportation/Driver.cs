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
        public Driver(string[] arr) // конструктор
        {
            name = arr[0]; exp = Convert.ToInt32(arr[1]);
        }
        public override string ToString() // переопределение метода ToString() для сохранения в файл
        {
            return $"{name}%{exp}";
        }
        public void PrintDriver() // метод для выведения информации о водителе на экран
        {
            Console.WriteLine($"{name}\t{exp}"); 
        }
        public static bool operator == (Driver driver1, Driver driver2) // оператор равенства
        {
            return (driver1.name == driver2.name && driver1.exp == driver2.exp);
        }
        public static bool operator !=(Driver driver1, Driver driver2)
        {
            return !(driver1 == driver2);
        }
    }
}
