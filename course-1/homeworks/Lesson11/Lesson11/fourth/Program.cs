//using System;

//namespace Lesson11.fourth
//{
//    class User
//    {
//        public string login {  get; set; }

//        private string password;

//        public User(string login, string password)
//        {
//            this.login = login;
//            Password = password;
//        }

//        public string Password
//        {
//            get
//            {
//                return password;
//            }
//            set
//            {
//                if(value.Length > 0)
//                {
//                    password = value;
//                    Console.WriteLine("Пороль обновлен");
//                }
//                else
//                {
//                    Console.WriteLine("Пароль не может быть пустым");
//                }
//            }
//        }

//        public void CheckPassword(string input)
//        {
//            if (input == this.Password)
//            {
//                Console.WriteLine("Доступ открыт");
//            }
//            else
//            {
//                Console.WriteLine("Пароль неверный. Доступ ограничен");
//            }
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            User user = new User("Sh@dow", "1234");
//            user.CheckPassword("password");
//            user.CheckPassword("1234");

//        }
//    }
//}