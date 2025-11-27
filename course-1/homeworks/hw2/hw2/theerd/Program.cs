using System;

namespace hw2.theerd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число номер 1: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите число номер 2: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            int summ = num1 + num2;
            int diff = num1 - num2;
            int mult = num1 * num2;
            int div = num1 / num2;

            Console.WriteLine(summ);
            Console.WriteLine(diff);
            Console.WriteLine(mult);
            Console.WriteLine(div);


        }
    }
}
