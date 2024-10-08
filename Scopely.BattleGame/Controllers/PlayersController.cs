using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scopely.BattleGame.Players;
using Scopely.BattleGame.Players.Services;

namespace Scopely.BattleGame.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayersService _playerService;

        public PlayersController(ILogger<PlayersController> logger, IPlayersService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        [HttpPost(Name = "CreatePlayer")]
        public async Task<IActionResult> CreatePlayer(NewPlayerRequest newPlayer)
        {
            try
            {
                await _playerService.CreatePlayer(newPlayer);
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
