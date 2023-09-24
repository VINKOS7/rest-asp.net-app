﻿using System.Net;

using MediatR;

using Library.Api.Responses;
using Library.Api.Options;
using Library.Api.Requests;
using Library.Domain.Aggregates.Account;
using Library.Passport.Api.Requests.Handlers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;


namespace Library.Passport.Api.Requests.AccountRequests.Handlers;

public class SignOutRequestHandler : IRequestHandler<SignOutRequest, SignOutResponse>
{
    private readonly JWTOptions _jwtOptions;
    private readonly IAccountRepo _accountRepo;
    //private readonly ISignOutTokenService _signOutTokenService;
    private readonly ILogger<SignInEmailRequestHandler> _logger;

    public SignOutRequestHandler(
        IAccountRepo accountRepo,
        //ISignOutTokenService signOutTokenService,
        IOptions<JWTOptions> jwtOptions,
        ILogger<SignInEmailRequestHandler> logger)
    {
        _accountRepo = accountRepo;
        //_signOutTokenService = signOutTokenService;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<SignOutResponse> Handle(SignOutRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();

            var dataToken = handler.ReadJwtToken(request.Token);

            var id = dataToken.Payload["_id"];

            var account = await _accountRepo.FindByIdAsync((Guid)id);

            if (account is null) throw new BadHttpRequestException("Do not touch token, my junior hacker))");

            var device = account.Devices.FirstOrDefault(d => d.Token == request.Token);

            if(device is not null) account.Devices.Remove(device);

            await _accountRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new SignOutResponse(Message: "signout");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting signout. Ex: {ex}");

            throw new HttpRequestException("Error with attempting signout.", ex, HttpStatusCode.InternalServerError);
        }
    }
}
