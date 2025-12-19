//using System;

//namespace hw5.first
//{
//    class Movie
//    {
//        public string Title { get; set; }
//        public string Genre { get; set; }
//        public int Raiting { get; set; }

//        public Movie() 
//        {
//            this.Title = "Неизвестно";
//            this.Genre = "Неизвестно";
//            this.Raiting = 0;
//        }

//        public Movie(string title)
//        {
//            this.Title = title;
//            this.Genre = "Неизвестно";
//            this.Raiting = 0;
//        }

//        public Movie(string title, string genre, int raiting) : this(title)
//        {
//            Genre = genre;
//            Raiting = raiting;
//        }
        
//        public void PrintInfo()
//        {
//            Console.WriteLine($"{this.Title} {this.Genre} {this.Raiting}");
//        }
//    }
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var m1 = new Movie(); 
//            var m2 = new Movie("Матрица");
//            var m3 = new Movie("Начало", "Фантастика", 9);

//            m1.PrintInfo();
//            m2.PrintInfo();
//            m3.PrintInfo();
//        }
//    }
//}