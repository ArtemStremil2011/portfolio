public class Reserve
{
    public Book ReservedBook { get; set; }
    public int ReaderID { get; set; }
    public DateTime DataReserved { get; set; }
    public DateTime DataEndOfReserve { get; set; }

    public Reserve(Book b, int rID, DateTime DateRes, DateTime DateEndOfRes)
    {
        this.ReservedBook = b;
        this.ReaderID = rID;
        this.DataReserved = DateRes;
        this.DataEndOfReserve = DateEndOfRes;
    }

    public bool IsExpired(DateTime currentDate)
    {
        return currentDate > DataEndOfReserve;
    }

    public void PrintInfo()
    {
        string activeStatus = IsExpired(DateTime.Now) ? "Нет" : "Да";
        Console.WriteLine($"Книга: \"{ReservedBook.Title}\", Читатель ID: {ReaderID}, Бронь с: {DataReserved:dd.MM.yyyy} по: {DataEndOfReserve:dd.MM.yyyy}, Активна: {activeStatus}");
    }
}