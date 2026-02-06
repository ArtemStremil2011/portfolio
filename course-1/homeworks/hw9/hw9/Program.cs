using System;
using System.Collections.Generic;
using System.IO;

namespace hw9
{
    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            //5. Реализуйте загрузку списка книг из файла books.txt при запуске программы.
            CreateFileIfNotExists();
            books = LoadBooks();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Меню управления книгами ---");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Показать все книги");
                Console.WriteLine("3. Сохранить и выйти");
                Console.WriteLine("4. Выйти без сохранения");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        ShowAllBooks();
                        break;
                    case "3":
                        SaveBooks(books);
                        Console.WriteLine("Книги сохранены. Выход...");
                        exit = true;
                        break;
                    case "4":
                        Console.WriteLine("Выход без сохранения...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void CreateFileIfNotExists()
        {
            if (!File.Exists("books.txt"))
            {
                File.Create("books.txt").Close();
            }
        }

        static List<Book> LoadBooks()
        {
            var loadedBooks = new List<Book>();

            try
            {
                foreach (var line in File.ReadAllLines("books.txt"))
                {
                    var parts = line.Split(';');

                    if (parts.Length == 2)
                    {
                        var book = new Book()
                        {
                            Title = parts[0].Trim(),
                            Author = parts[1].Trim()
                        };
                        loadedBooks.Add(book);
                    }
                }
            }
            catch (Exception)
            {
            }

            return loadedBooks;
        }

        static void AddBook()
        {
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();

            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(author))
            {
                books.Add(new Book { Title = title, Author = author });
                Console.WriteLine("Книга успешно добавлена!");
            }
            else
            {
                Console.WriteLine("Название и автор не могут быть пустыми!");
            }
        }

        static void ShowAllBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Список книг пуст.");
                return;
            }

            Console.WriteLine("\nСписок всех книг:");
            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. \"{books[i].Title}\" - {books[i].Author}");
            }
        }

        static void SaveBooks(List<Book> books)
        {
            try
            {
                using (var writer = new StreamWriter("books.txt"))
                {
                    foreach (var book in books)
                    {
                        writer.WriteLine($"{book.Title};{book.Author}");
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}