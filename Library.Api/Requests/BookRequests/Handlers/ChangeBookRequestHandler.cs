using Library.Domain.Aggregates.Book;
using MediatR;

namespace Library.Api.Requests.Handlers.BookHandlers;

public class ChangeBookRequestHandler : IRequestHandler<ChangeBookRequest>
{
    private readonly IBookRepo _bookRepo;
    private readonly ILogger<ChangeBookRequestHandler> _logger;

    public ChangeBookRequestHandler(IBookRepo bookRepo, ILogger<ChangeBookRequestHandler> logger)
    {
        _bookRepo = bookRepo;
        _logger = logger;
    }

    public async Task Handle(ChangeBookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepo.FindByIdAsync(request.Id);

            if (book is null) throw new BadHttpRequestException("");

            book.Change(request);

            await _bookRepo.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting change book, Ex: {ex}");

            throw;
        }
    }
}
