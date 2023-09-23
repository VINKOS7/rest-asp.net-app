using Dotseed.Domain;
using Library.Domain.Aggregates.Book.Commands;

namespace Library.Domain.Aggregates.Book;

public class Book : Entity, IChangePropsBook, IAggregateRoot
{
    public static Book From(IAddBookCommand command)
    {
        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Genre = command.Genre,
            Author = command.Author,
            DateOfWritten = DateTime.SpecifyKind(command.DateOfWritten, DateTimeKind.Utc)
        };

        book.SetCreatedAt(DateTime.UtcNow);
        book.SetUpdateAt(DateTime.UtcNow);

        return book;
    }

    public string Name { get; private set; }
    public string Genre { get; private set; }
    public string Author { get; private set; }
    public DateTime DateOfWritten { get; private set; }

    public void Change(IChangeBookCommand command)
    {
        Name = command.Name;
        Genre = command.Genre;
        Author = command.Author;
        DateOfWritten = command.DateOfWritten;
    }
}
