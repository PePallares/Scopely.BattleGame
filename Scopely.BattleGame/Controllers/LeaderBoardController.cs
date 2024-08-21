using Microsoft.AspNetCore.Mvc;
using Scopely.BattleGame.LeaderBoards.Services;

namespace Scopely.BattleGame.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderBoardController : Controller
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly ILeaderBoardsService _leaderBoardsService;

        public LeaderBoardController(ILogger<PlayersController> logger, ILeaderBoardsService leaderBoardsService)
        {
            _logger = logger;
            _leaderBoardsService = leaderBoardsService;
        }


        [HttpGet(Name = "GetLeaderBoard")]
        public async Task<IActionResult> GetLeaderBoard(string leaderBoardName)
        {
            try
            {
                var leaderBoard = await _leaderBoardsService.GetLeaderBoard(leaderBoardName);
                return Ok(leaderBoard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
