using System;

namespace hw6
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Тестирование библиотечной системы ===\n");

            Library library = new Library();

            Textbook textbook = new Textbook("Математика 101", "Иванов", 2020, 300, "Математика");
            FictionBook fictionBook = new FictionBook("Война и мир", "Толстой", 1869, 1200, "Роман");
            Book simpleBook = new Book("Программирование на C#", "Петров", 2023, 450);

            library.AddBook(textbook);
            library.AddBook(fictionBook);
            library.AddBook(simpleBook);

            library.ShowAllBooks();

            Reader reader = new Reader("Алексей", 1);
            reader.ShowInfo();

            Console.WriteLine("\n=== Выдача книги ===");
            Book bookToIssue = library.FindBook("Математика 101");
            library.IssueBook(bookToIssue, reader);

            library.ShowAllBooks();
            reader.ShowInfo();

            Console.WriteLine("\n=== Возврат книги ===");
            if (reader.ReturnBook("Математика 101"))
            {
                library.AddBook(textbook);
            }

            library.ShowAllBooks();
            reader.ShowInfo();

            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ БРОНИРОВАНИЯ ===");

            Reader reader1 = new Reader("Иван", 1);
            Reader reader2 = new Reader("Мария", 2);

            bool reserveSuccess = library.ReserveBook(textbook, reader1, 5);
            Console.WriteLine($"Резервирование учебника: {(reserveSuccess ? "УСПЕХ" : "ПРОВАЛ")}");

            library.ShowAllReserves();

            Console.WriteLine("\nПопытка выдачи зарезервированной книги другому читателю:");
            library.IssueBook(textbook, reader2);

            Console.WriteLine("\nВыдача книги читателю, который её резервировал:");
            library.IssueBook(textbook, reader1);

            library.ShowAllBooks();
            reader1.ShowInfo();
            reader2.ShowInfo();
        }
    }
}