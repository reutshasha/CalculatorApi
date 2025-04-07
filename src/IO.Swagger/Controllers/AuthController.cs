using BL.Interfaces;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CalculatorApi.Controllers
{
    ///// <summary>
    ///// Login and obtain a JWT token
    ///// </summary>
    ///// <remarks>Accepts a username and password and returns a JWT token for authentication.</remarks>
    ///// <param name="body"></param>
    ///// <response code="200">Successful login</response>
    ///// <response code="401">Invalid login credentials</response>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthManager authManager, ILogger<AuthController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginDto)
        {
            return _authManager.Authenticate(loginDto.Username, loginDto.Password) is string token
                ? Ok(new { Token = token })
                : Unauthorized(new { Error = "Invalid username or password." });
        }

    }
}
