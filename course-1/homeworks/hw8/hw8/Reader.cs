public class Reader
{
    public string Name;
    public int ID;
    private List<Book> Books;

    public Reader()
    {
        Name = "Аноним";
        ID = 0;
        Books = new List<Book>();
    }

    public Reader(string name, int id)
    {
        Name = name;
        ID = id;
        Books = new List<Book>();
    }

    public Reader(string name, int id, List<Book> books)
    {
        Name = name;
        ID = id;
        Books = books ?? new List<Book>();
    }

    public Reader(Reader other)
    {
        Name = other.Name;
        ID = other.ID;
        Books = new List<Book>(other.Books);
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"{Name} взял книгу: {book.Title}");
    }

    public bool ReturnBook(string title)
    {
        Book bookToReturn = Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (bookToReturn != null)
        {
            Books.Remove(bookToReturn);
            Console.WriteLine($"{Name} вернул книгу: {title}");
            return true;
        }
        return false;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"\nЧитатель: {Name}, ID: {ID}");
        if (Books.Count == 0)
        {
            Console.WriteLine("Нет книг на руках");
        }
        else
        {
            Console.WriteLine($"Книги на руках ({Books.Count}):");
            foreach (Book b in Books)
            {
                Console.WriteLine("  " + b.GetInfo());
            }
        }
    }

    public bool HasBook(string title)
    {
        return Books.Any(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }
}