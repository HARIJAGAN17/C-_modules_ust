using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Basic_auth.Authneticator
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];

            if(string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            if(!authorizationHeader.StartsWith("basic ",StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = authorizationHeader.Substring(6);

            var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var credentials = credentialString.Split(":");

            var username = credentials[0];
            var password = credentials[1];

            if(username == "harijagan" && password == "idkPassword") {

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,username),
                };
                var idnetity = new ClaimsIdentity(claims, "basic");
                var claimPrincipal = new ClaimsPrincipal(idnetity);

                return AuthenticateResult.Success(new AuthenticationTicket(claimPrincipal, Scheme.Name));

          
            }

            return AuthenticateResult.Fail("Unauthorized");
        }
    }
}
