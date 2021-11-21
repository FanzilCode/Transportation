using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation
{
    class Pay
    {
        public string name { get; private set; } // ФИО
        public double routePay { get; set; } // оплата за маршруты
        public double reward { get; set; } // премия

        public double sum { get; set; } // оплата с учетом премии
        public Pay(string name, double routePay, double reward) // конструктор
        {
            this.name = name; this.routePay = routePay; this.reward = reward; sum = routePay + reward;
        }
        public void Add(double pay, double reward) // метод для накопления общей заработанной суммы
        {
            this.routePay += pay;
            this.reward += reward;
            this.sum += pay + reward;
        }
        public void PrintPay() // метод для выведения на экран информации об оплатах
        {
            Console.WriteLine($"{name}\t\t{routePay}\t\t{reward}\t\t{sum}");
        }
        public static bool operator == (Pay pay1, Pay pay2) // оператор равенства
        {
            return (pay1.name == pay2.name);
        }
        public static bool operator !=(Pay pay1, Pay pay2)
        {
            return !(pay1 == pay2);
        }
    }
}
