namespace Library.Domain.Aggregates.Book;

public interface IChangePropsBook
{
    public string Name { get;}
    public string Genre { get; }
    public string Author { get; }
    public DateTime DateOfWritten { get; }
}
