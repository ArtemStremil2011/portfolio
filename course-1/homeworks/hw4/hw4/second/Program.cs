using System;

namespace hw4
{
    class Phone
    {
        public string Model { get; set; }

        private int battery;
        public int Battery
        {
            get { return battery; }
            private set
            {
                battery = value;
                if (battery > 100) battery = 100;
                if (battery < 0) battery = 0;
            }
        }

        public Phone(string model = "Unknown")
        {
            Model = model;
            Battery = 50;
        }

        public void Charge(int amount)
        {
            if (amount > 0)
            {
                Battery += amount;
                Console.WriteLine($"Телефон заряжен на {amount}%");
            }
        }

        public void Use(int amount)
        {
            if (amount > 0)
            {
                Battery -= amount;
                Console.WriteLine($"Телефон использован на {amount}%");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var phone = new Phone { Model = "Samsung Galaxy" };

            phone.Charge(30);
            Console.WriteLine($"Заряд: {phone.Battery}%");

            phone.Use(10);
            Console.WriteLine($"Заряд: {phone.Battery}%");
        }
    }
}