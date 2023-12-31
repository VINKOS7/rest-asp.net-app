﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using Library.Api.Requests;
using Library.Api.Responses;
using Microsoft.AspNetCore.Cors;

namespace Library.Api.Controllers;

[Route("book")]
public class BookController : Controller
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator) => _mediator = mediator;
    

    [AllowAnonymous, HttpGet("fetch")]
    public async Task<FetchBooksResponse> Fetch([FromQuery] int offset = 0, [FromQuery] int size = 20) => await _mediator.Send(new FetchBooksRequest(offset, size));


    [Authorize, HttpPost("add")]
    public async Task<Guid> Add([FromBody] AddBookRequest request) => await _mediator.Send(request);


    [Authorize, HttpPost("change")]
    public async Task Change([FromBody] ChangeBookRequest request) => await _mediator.Send(request);


    [Authorize, HttpDelete("delete")]
    public async Task Delete([FromQuery] Guid id) => await _mediator.Send(new DeleteBookByIdRequest(id));
}
