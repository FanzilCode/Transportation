using System;

namespace Transportation
{
    class Driver
    {
        public string lastName { get; private set; } // Фамилия
        public string firstName { get; private set; } // Имя
        public string midName { get; private set; } // Отчество
        public int exp { get; private set; } // стаж (кол-во лет)
        public string fullName { get; private set; } // ФИО

        public Driver(string lastName, string firstName, string midName, int exp) // конструктор
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.midName = midName;
            this.exp = exp;
            this.fullName = $"{lastName} {firstName} {midName}";
        }
        public Driver(string[] arr) // конструктор
        {
            lastName = arr[0];
            firstName = arr[1];
            midName = arr[2];
            exp = Convert.ToInt32(arr[3]);
        }
        public override string ToString() // переопределение метода ToString() для сохранения в файл
        {
            return $"{lastName}%{firstName}%{midName}%{exp}";
        }
        public void PrintDriver() // метод для выведения информации о водителе на экран
        {
            Console.WriteLine($"{lastName} {firstName} {midName}\t{exp}"); 
        }
        public static bool operator == (Driver driver1, Driver driver2) // оператор равенства
        {
            return (driver1.lastName == driver2.lastName && driver1.firstName == driver2.firstName && driver1.midName == driver2.midName && driver1.exp == driver2.exp);
        }
        public static bool operator !=(Driver driver1, Driver driver2)
        {
            return !(driver1 == driver2);
        }
    }
}
