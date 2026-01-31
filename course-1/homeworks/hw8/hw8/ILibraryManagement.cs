public interface ILibraryManagement
{
    void AddBook(Book book);
    bool RemoveBook(string title);
    Book FindBook(string title);
    void ShowAllBooks();
}