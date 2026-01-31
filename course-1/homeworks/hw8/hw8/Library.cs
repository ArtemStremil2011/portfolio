public class Library : ILibraryManagement
{
    private List<Book> books;
    public List<Reserve> reserves;

    public Library()
    {
        books = new List<Book>();
        reserves = new List<Reserve>();
    }

    public bool IsBookReserved(Book book)
    {
        return reserves.Any(r => r.ReservedBook == book && !r.IsExpired(DateTime.Now));
    }

    public Reserve GetActiveReservation(Book book)
    {
        return reserves.FirstOrDefault(r => r.ReservedBook == book && !r.IsExpired(DateTime.Now));
    }

    public bool ReserveBook(Book book, Reader reader, int days = 7)
    {
        if (book == null || reader == null)
        {
            Console.WriteLine("Ошибка: книга или читатель не указаны");
            return false;
        }

        if (!books.Contains(book))
        {
            Console.WriteLine($"Ошибка: книги \"{book.Title}\" нет в библиотеке");
            return false;
        }

        Reserve activeRes = GetActiveReservation(book);
        if (activeRes != null && activeRes.ReaderID != reader.ID)
        {
            Console.WriteLine($"Ошибка: книга уже зарезервирована читателем ID: {activeRes.ReaderID}");
            return false;
        }

        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddDays(days);
        Reserve newReserve = new Reserve(book, reader.ID, startDate, endDate);

        reserves.Add(newReserve);

        Console.WriteLine($"Книга \"{book.Title}\" забронирована для {reader.Name} до {endDate:dd.MM.yyyy}");

        return true;
    }

    public void ShowAllReserves()
    {
        Console.WriteLine("\n=== Все бронирования ===");
        if (reserves.Count == 0)
        {
            Console.WriteLine("Бронирований нет.");
            return;
        }

        foreach (Reserve reserve in reserves)
        {
            reserve.PrintInfo();
        }
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public bool RemoveBook(string title)
    {
        Book book = FindBook(title);
        if (book != null)
        {
            books.Remove(book);
            return true;
        }
        return false;
    }

    public Book FindBook(string title)
    {
        return books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public void ShowAllBooks()
    {
        Console.WriteLine("\n=== Все книги в библиотеке ===");
        if (books.Count == 0)
        {
            Console.WriteLine("Библиотека пуста");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i].GetInfo()}");
        }
    }

    public void IssueBook(Book book, Reader reader)
    {
        if (book == null)
        {
            Console.WriteLine("Ошибка: книга не найдена");
            return;
        }

        if (!books.Contains(book))
        {
            Console.WriteLine($"Ошибка: книги \"{book.Title}\" нет в библиотеке");
            return;
        }

        Reserve activeReservation = GetActiveReservation(book);
        if (activeReservation != null && activeReservation.ReaderID != reader.ID)
        {
            Console.WriteLine($"Ошибка: книга \"{book.Title}\" зарезервирована другим читателем (ID: {activeReservation.ReaderID})");
            return;
        }

        books.Remove(book);
        reader.AddBook(book);
        Console.WriteLine($"Книга \"{book.Title}\" выдана читателю {reader.Name}");
    }

    public void ReturnBook(Book book)
    {
        if (book == null)
        {
            Console.WriteLine("Ошибка: книга не найдена");
            return;
        }

        books.Add(book);
        Console.WriteLine($"Книга \"{book.Title}\" возвращена в библиотеку");
    }
}