using ApplicationCore.Helpers;
using ApplicationCore.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration config, IAuthService authService)
        {
            _config = config;
            _authService = authService;
        }

        // GET: api/Auth/token
        [HttpGet("token/{clientId}/{clientSecret}")]
        public async Task<ActionResult> Token(string clientId, string clientSecret)
        {
            ActionResult response = Unauthorized();

            if (clientSecret.IsBase64())
            {
                var tokens = await _authService.GenerateJwtAsync(clientId, clientSecret.ToBase64DecodeWithKey(_config["Security:EncryptKey"]));

                if (tokens.Count > 0)
                {
                    response = Ok(new { token = tokens[0], validFrom = tokens[1], validTo = tokens[2] });
                }
            }

            return response;
        }
    }
}
