using JwtToken.API.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtToken.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IJwtService _jwtService;
        public AuthenticationController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("auth")]
        public async Task<IActionResult> Auth()
        {
            var result = _jwtService.GenerateToken();
            return Ok(result);
        }
    }
}
