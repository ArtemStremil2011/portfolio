//using System;

//namespace hw4.first
//{
//    class Calculator
//    {
//        public double num1;
//        public double num2;
//        public string op;

//        public Calculator(double value1, double value2, string op)
//        {
//            num1 = value1;
//            num2 = value2;
//            this.op = op;
//        }

//        public double Summ(double num1, double num2)
//        {
//            return num1 + num2;
//        }

//        public double Diff(double num1, double num2)
//        {
//            return num1 - num2;
//        }

//        public double Mult(double num1, double num2)
//        {
//            return num1 * num2;
//        }

//        public string Div(double num1, double num2)
//        {
//            if (num2 == 0)
//            {
//                return "Ошибка! Деление на 0 невозможно";
//            }
//            else
//            {
//                return Convert.ToString(num1 / num2);
//            }
//        }

//        public void Calc()
//        {
//            switch (op)
//            {
//                case "+":
//                    Console.WriteLine($"Результат: {Summ(num1, num2)}");
//                    break;
//                case "-":
//                    Console.WriteLine($"Результат: {Diff(num1, num2)}");
//                    break;
//                case "/":
//                    Console.WriteLine($"Результат: {Div(num1, num2)}");
//                    break;
//                case "*":
//                    Console.WriteLine($"Результат: {Mult(num1, num2)}");
//                    break;
//                default:
//                    Console.WriteLine("Ошибка: Неизвестная операция");
//                    break;
//            }
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            while (true)
//            {
//                Console.Write("Введите выражение или exit: ");
//                string expression = Console.ReadLine();

//                if (expression == "exit")
//                {
//                    break;
//                }
//                else
//                {
//                    string[] expressions = expression.Split(" ");

//                    if (expressions.Length != 3)
//                    {
//                        Console.WriteLine("Ошибка: Неверный формат выражения. Используйте: число оператор число");
//                        continue;
//                    }

//                    double num1 = Convert.ToDouble(expressions[0]);
//                    double num2 = Convert.ToDouble(expressions[2]);
//                    string op = expressions[1];

//                    Calculator calc = new Calculator(num1, num2, op);
//                    calc.Calc();
//                }
//            }
//        }
//    }
//}