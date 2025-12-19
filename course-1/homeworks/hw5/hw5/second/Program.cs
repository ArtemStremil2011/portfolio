//using System;

//namespace hw5.second
//{
//    class Devise
//    {
//        public string Name { get; set; }

//        public void TurnOn()
//        {
//            Console.WriteLine("Устройство включено.");
//        }
//    }

//    class Kettle : Devise
//    {
//        public void Boil()
//        {
//            Console.WriteLine("Чайник кипятит воду.");
//        }
//    }

//    class Toaster : Devise
//    {
//        public void Toast()
//        {
//            Console.WriteLine("Тостер поджаривает хлеб.");
//        }
//    }
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var kettle = new Kettle();
//            kettle.Name = "Redmond";
//            kettle.TurnOn(); 
//            kettle.Boil();
//            var toaster = new Toaster();
//            toaster.Name = "Philips";
//            toaster.TurnOn();
//            toaster.Toast();

//        }
//    }
//}
