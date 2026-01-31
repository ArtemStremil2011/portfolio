public class Textbook : Book
{
    public string Subject { get; private set; }

    public Textbook(string title, string author, int year, int pages, string subject)
        : base(title, author, year, pages)
    {
        Subject = subject;
    }

    public override string GetInfo()
    {
        return $"Учебник: \"{Title}\" - {Author}, {Year} год, {Pages} стр., Предмет: {Subject}";
    }
}