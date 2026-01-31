public class Book : Publication
{
    private int year;
    private int pages;

    public int Year => year;
    public int Pages => pages;

    public Book() : base("Неизвестно", "Неизвестно")
    {
        year = 0;
        pages = 0;
    }

    public Book(string title, string author, int year, int pages)
        : base(title, author)
    {
        this.year = year;
        this.pages = pages;
    }

    public Book(string title, string author)
        : base(title, author)
    {
        year = DateTime.Now.Year;
        pages = 100;
    }

    public override string GetInfo()
    {
        return $"Книга: \"{Title}\" - {Author}, {year} год, {pages} стр.";
    }
}