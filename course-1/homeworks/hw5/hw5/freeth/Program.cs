using System;

namespace hw5.second
{
    class Devise
    {
        public string Name { get; set; }

        public void TurnOn()
        {
            Console.WriteLine("Устройство включено.");
        }

        public virtual void Beep() 
        {
            Console.WriteLine("Устройство подаёт сигнал."); 
        }

    }

    class Kettle : Devise
    {
        public void Boil()
        {
            Console.WriteLine("Чайник кипятит воду.");
        }

        public override void Beep()
        {
            Console.WriteLine("Чайник пикнул: пи-пи!");
        }
    }

    class Toaster : Devise
    {
        public void Toast()
        {
            Console.WriteLine("Тостер поджаривает хлеб.");
        }

        public override void Beep()
        {
            Console.WriteLine("Тостер пикнул: динь!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var k = new Kettle(); 
            var t = new Toaster(); 

            k.Beep(); 
            t.Beep();

        }
    }
}