public class FictionBook : Book
{
    public string Genre { get; private set; }

    public FictionBook(string title, string author, int year, int pages, string genre)
        : base(title, author, year, pages)
    {
        Genre = genre;
    }

    public override string GetInfo()
    {
        return $"Художественная книга: \"{Title}\" - {Author}, {Year} год, {Pages} стр., Жанр: {Genre}";
    }
}