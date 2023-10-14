using Library.Api.Responses;
using Library.Domain.Aggregates.Book;
using MediatR;

namespace Library.Api.Requests.Handlers.BookHandlers;

public class AddBookRequestHandler : IRequestHandler<AddBookRequest, Guid>
{
    private readonly IBookRepo _bookRepo;
    private readonly ILogger<AddBookRequestHandler> _logger;

    public AddBookRequestHandler(IBookRepo bookRepo, ILogger<AddBookRequestHandler> logger)
    {
        _bookRepo = bookRepo;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddBookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var book = Book.From(request);

            if (book is null) throw new BadHttpRequestException("bad obj");

            await _bookRepo.AddAsync(book);

            await _bookRepo.UnitOfWork.SaveEntitiesAsync();

            return book.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting add new book, Ex: {ex}");

            throw;
        }
    }
}
