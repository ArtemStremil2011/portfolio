//using System;

//namespace first
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int[] grades = {1, 2, 3, 4, 5, 4, 2};
//            double midl = (double)summary(grades) / (double)grades.Length;
//            int fives = find_num(grades, 5);
//            int twoes = find_num(grades, 2);
//            Console.WriteLine("Средняя оценка: " + midl);
//            Console.WriteLine("Кол-во пятерок: " + fives);
//            Console.WriteLine("Кол-во двоек: " + twoes);
            
//        }

//        static int summary(int[] arr)
//        {
//            int summary = 0;
//            foreach(int num in arr)
//            {
//                summary += num;
//            }
//            return summary;
//        }

//        static int find_num(int[] arr, int num)
//        {
//            int count = 0;
//            foreach(int number in arr)
//            {
//                if (number == num)
//                {
//                    count++;
//                }
//            }
//            return count;
//        }
//    }
//}