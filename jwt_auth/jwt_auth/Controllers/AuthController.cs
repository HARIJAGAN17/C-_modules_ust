using jwt_auth.Authenticate;
using jwt_auth.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt_auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IJwtTokenManager _jwtToken;
        public AuthController(IJwtTokenManager jwtTokenManager)
        {
            _jwtToken = jwtTokenManager;
        }

        [HttpPost]

        public IActionResult GetToken([FromBody] Login loginCredentials)
        {
            if(loginCredentials is { Username: "harijagan", Password: "Password@123" })
            {
                var token = _jwtToken.Authenticate(loginCredentials.Username);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
