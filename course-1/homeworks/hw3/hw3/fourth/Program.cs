using System;

namespace fourth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password;
            int count = 3;

            do
            {
                Console.Write($"Введите пароль (осталось попыток: {count}): ");
                password = Console.ReadLine();
                count--;

            } while (count > 0 && password != "1234");

            if (password == "1234")
            {
                Console.WriteLine("Доступ открыт");
            }
            else
            {
                Console.WriteLine("Доступ запрещен");
            }
        }
    }
}