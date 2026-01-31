public abstract class Publication
{
    public string Title { get; protected set; }
    public string Author { get; protected set; }

    public Publication(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public abstract string GetInfo();
}