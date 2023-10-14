using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using Library.Api.Responses;
using Library.Api.Requests;
using MediatR;
using Library.Domain.Aggregates.Book;
using Library.Infrastructure;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Library.Api.Controllers.Hubs;


public class BookHub : Hub
{
    private readonly IMediator _mediator;

    public BookHub(IMediator mediator) => _mediator = mediator;

    public async Task Send(string name, string message)
    {
        // Call the broadcastMessage method to update clients.
        await Clients.All.SendAsync("broadcastMessage", name, message);
    }

    public async Task FetchParallel(int offset = 3, int size = 20)
    {
        var send = async (Book book, int mililiseconds) => await Clients.Caller.SendAsync("fetchBooks", new BookParallelResponse(book, mililiseconds));

        await _mediator.Send(new FetchBooksParallelRequest(offset, size) { Send = send });
    }
}
