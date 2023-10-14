using Library.Api.Responses;
using Library.Domain.Aggregates.Book;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace Library.Api.Requests.Handlers.BookHandlers;

public class FetchBooksRequestParallelHandler : IRequestHandler<FetchBooksParallelRequest>
{
    private readonly IBookRepo _bookRepo;
    private readonly ILogger<AddBookRequestHandler> _logger;

    public FetchBooksRequestParallelHandler(IBookRepo bookRepo, ILogger<AddBookRequestHandler> logger)
    {
        _bookRepo = bookRepo;
        _logger = logger;
    }

    public async Task Handle(FetchBooksParallelRequest request, CancellationToken cancellationToken)
    {
        try
        {
            for (int i = 0; i < request.Size; ++i) 
            {
                var book = (await _bookRepo.FetchAsync(request.Offset, request.Size + i)).ElementAt(0);
               
                new Thread(() =>
                {
                    var imitationTimeTask = new Random().Next(000, 999);

                    Task.Delay(imitationTimeTask).Wait();

                    request.Send(book, imitationTimeTask);
                }).Start();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting fetch books, Ex: {ex}");

            throw;
        }
    }
}
