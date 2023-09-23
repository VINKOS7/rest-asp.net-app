using Library.Domain.Aggregates.Book;
using Newtonsoft.Json;

namespace Library.Api.Responses;

public class BookResponse
{
    public BookResponse(Book book)
    {
        Id = book.Id;
        Name = book.Name;
        Genre = book.Genre;
        Author = book.Author;
        DateOfWritten = book.DateOfWritten;
    }

    [JsonProperty("id")]
    public Guid Id { get; private set; }

    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("genre")]
    public string Genre { get; private set; }

    [JsonProperty("name")]
    public string Author { get; private set; }

    [JsonProperty("dateOfWritten")]
    public DateTime DateOfWritten { get; private set; }
}

public record FetchBooksResponse([JsonProperty("books")] ICollection<BookResponse> Books);