using Library.Api.Responses;
using Library.Domain.Aggregates.Book;
using MediatR;

namespace Library.Api.Requests.Handlers.BookHandlers;

public class FetchBooksRequestHandler : IRequestHandler<FetchBooksRequest, FetchBooksResponse>
{
    private readonly IBookRepo _bookRepo;
    private readonly ILogger<AddBookRequestHandler> _logger;

    public FetchBooksRequestHandler(IBookRepo bookRepo, ILogger<AddBookRequestHandler> logger)
    {
        _bookRepo = bookRepo;
        _logger = logger;
    }

    public async Task<FetchBooksResponse> Handle(FetchBooksRequest request, CancellationToken cancellationToken)
    {
        try
        {         
            var books = await _bookRepo.FetchAsync(request.Offset, request.Size);

            return new FetchBooksResponse(books.Select(b => new BookResponse(b)).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting fetch books, Ex: {ex}");

            throw;
        }
    }
}