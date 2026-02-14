using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace hw10
{
    class Program
    {
        public static void Main(string[] args)
        {
            string jsonAuthors = File.ReadAllText("authors.json");
            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(jsonAuthors);

            string jsonBooks = File.ReadAllText("Books.json");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonBooks); 

            // 1. Топ-3 книги по цене (не старше 1950)
            List<Book> topThreeBook = books
                .Where(b => b.year >= 1950)
                .OrderByDescending(b => b.price)
                .Take(3)
                .ToList();

            Console.WriteLine("1. Топ-3 книги по цене (не старше 1950 года):");
            foreach (var book in topThreeBook)
            {
                Console.WriteLine($"   {book.title} - {book.price} руб. ({book.year} г.)");
            }
            Console.WriteLine();

            // 2. Список строк «Название (Год) — Цена»
            var bookString = books
                .Select(b => $"{b.title} ({b.year}) — {b.price} руб.")
                .OrderBy(b => b)
                .ToList();

            Console.WriteLine("2. Книги в формате «Название (Год) — Цена»:");
            foreach (var item in bookString)
            {
                Console.WriteLine($"   {item}");
            }
            Console.WriteLine();

            // 3. Книги с авторами и странами
            var booksWithAuthors = books
                .Join(authors,
                      book => book.author,
                      author => author.name,
                      (book, author) => new
                      {
                          Title = book.title,
                          AuthorName = author.name,
                          Country = author.country
                      })
                .OrderBy(b => b.AuthorName)
                .Select(x => $"{x.Title} — {x.AuthorName} ({x.Country})")
                .ToList();

            Console.WriteLine("3. Книги с авторами и странами:");
            foreach (var item in booksWithAuthors)
            {
                Console.WriteLine($"   {item}");
            }
            Console.WriteLine();

            // 4. Все уникальные теги
            var allTags = books
                .SelectMany(b => b.tags)
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            Console.WriteLine("4. Все уникальные теги (по алфавиту):");
            foreach (var tag in allTags)
            {
                Console.WriteLine($"   - {tag}");
            }
        }
    }
}