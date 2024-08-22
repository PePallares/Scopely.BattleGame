using Microsoft.AspNetCore.Mvc;
using Scopely.BattleGame.JWT.Services;

namespace JwtInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IJWTokenService _JWTokenService;

        public LoginController(ILogger<LoginController> logger, IJWTokenService jWTokenService)
        {
            _logger = logger;
            _JWTokenService = jWTokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userId)
        {
            try
            {
                var token = await _JWTokenService.GetJWToken(userId);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}