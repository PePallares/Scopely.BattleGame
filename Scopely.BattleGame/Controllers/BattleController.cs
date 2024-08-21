using Microsoft.AspNetCore.Mvc;
using Scopely.BattleGame.Battles.Services;

namespace Scopely.BattleGame.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleController : Controller
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IBattleService _battlesService;

        public BattleController(ILogger<PlayersController> logger, IBattleService battlesService)
        {
            _logger = logger;
            _battlesService = battlesService;
        }

        [HttpPost(Name = "SubmitBattle")]
        public async Task<IActionResult> SubmitBattle(string attackerId, string defenderId)
        {
            try
            {
                await _battlesService.SubmitBattle(attackerId, defenderId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
