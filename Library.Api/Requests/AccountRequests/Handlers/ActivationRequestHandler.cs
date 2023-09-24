using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

using MediatR;

using Library.Api.Responses;
using Library.Api.Options;
using Library.Api.Controllers;
using Library.Api.Requests;
using Library.Domain.Aggregates.Account;
using Library.Domain.Aggregates.Account.Enums;
using Library.Domain.Services.UserAgentParser;
using Library.Passport.Api.Requests.AccountRequests.Handlers;


namespace Library.Passport.Api.Requests.Handlers;

public class ActivationRequestHandler : IRequestHandler<ActivationRequest, ActivationResponse>
{
    private readonly JWTOptions _jwtOptions;
    private readonly IAccountRepo _accountRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AccountController> _logger;

    public ActivationRequestHandler(
        IAccountRepo accountRepo,
        IHttpContextAccessor httpContextAccessor,
        IOptions<JWTOptions> jwtOptions,
        ILogger<AccountController> logger)
    {
        _accountRepo = accountRepo;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<ActivationResponse> Handle(ActivationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _accountRepo.FindByActivationCodeAsync(request.Code);

            if (account is null) throw new BadHttpRequestException("Account not found");

            if (account.AccessStatus != AccessStatus.WaitActivate) throw new BadHttpRequestException("Account not wait activate");

            var jwtToken = new JwtToken(account.Id, _jwtOptions.SecretKey, _jwtOptions.Issuer, _jwtOptions.ExpiresHours).Value;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();

            var operationSystem = UserAgentParser.GetOperatingSystem(userAgent);

            account.ActivationCode = string.Empty;
            account.AccessStatus = AccessStatus.Active;

            if (account.Devices is null) account.Devices = new();

            account.Devices.Add(new()
            {
                Id = Guid.NewGuid(),
                Name = operationSystem,
                Version = UserAgentParser.GetOsVersion(userAgent, userAgent),
                Token = encodedJwt,
                OnlineAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            });

            await _accountRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new ActivationResponse(Token: encodedJwt, Message: "Access true");
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error with signin. Error: {ex}");

            throw;
        }

    }
}
