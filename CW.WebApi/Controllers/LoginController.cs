using CW.Domain;
using CW.WebApi.Abstractions.Services.Authentication;
using CW.WebApi.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace CW.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginUserRequestModel login)
        {
            try
            {
                var token = _authenticationService.Authenticate(login);
                return Ok(new
                {
                    token = token.Item1,
                    user = token.Item2,
                });
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
           
        }
    }
}
