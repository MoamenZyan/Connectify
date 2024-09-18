using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Connectify.Infrastructure.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                            ILoggerFactory logger,
                                            UrlEncoder encoder,
                                            ISystemClock clock,
                                            IUserRepository userRepository,
                                            IUserService userService) : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.NoResult();

            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authHeader))
                return AuthenticateResult.NoResult();

            if (!authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Unknown scheme");

            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter!)).Split(":");
            UserCredentials userCredentials = new UserCredentials()
            {
                Email = credentials[0],
                Password = credentials[1]
            };


            var result = await _userService.ValidateUserCredentials(userCredentials, _userRepository.GetUserByEmailAsync);
            if (!result.Item1)
                return AuthenticateResult.Fail("Forbiden");

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, result.Item2!)
            }, authHeader.Scheme);

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, authHeader.Scheme);

            return AuthenticateResult.Success(ticket);
        }
    }
}
