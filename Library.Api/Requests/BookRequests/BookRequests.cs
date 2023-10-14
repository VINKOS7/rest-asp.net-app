using MediatR;
using Newtonsoft.Json;

using Library.Domain.Aggregates.Book;
using Library.Domain.Aggregates.Book.Commands;
using Library.Api.Responses;
using Microsoft.AspNetCore.SignalR;

namespace Library.Api.Requests;

public record AddBookRequest(
    [JsonProperty("name")] string Name,
    [JsonProperty("genre")] string Genre,
    [JsonProperty("author")] string Author,
    [JsonProperty("dateOfWritten")] DateTime DateOfWritten
)
: IAddBookCommand, IRequest<Guid>;

public record ChangeBookRequest(
    [JsonProperty("id")] Guid Id,
    [JsonProperty("name")] string Name,
    [JsonProperty("genre")] string Genre,
    [JsonProperty("author")] string Author,
    [JsonProperty("dateOfWritten")] DateTime DateOfWritten
)
: IChangeBookCommand, IRequest;

public record FetchBooksRequest(
    [JsonProperty("offset")] int Offset,
    [JsonProperty("size")] int Size
) 
: IRequest<FetchBooksResponse>;

public record FetchBooksParallelRequest(
    [JsonProperty("offset")] int Offset,
    [JsonProperty("size")] int Size
)
: IRequest
{
    public Func<Book, int, Task> Send;
}
public record DeleteBookByIdRequest([JsonProperty("id")] Guid Id) : IRequest;