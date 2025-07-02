using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.Services;

namespace RockPaperScissors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IGameRulesService _gameRulesService;

        private static Move lastPlayerMove;
        public GameController(IGameService gameService, IGameRulesService gameRulesService)
        {
            _gameService = gameService;
            _gameRulesService = gameRulesService;
        }

        [HttpGet("play")]
        public IActionResult Play(string playerMove, string mode = "classic", string computerMode = "random")
        {
            if (!Enum.TryParse<Move>(playerMove, true, out var player))
            {
                return BadRequest("Invalid player move.");
            }

            var gameService = new GameService();

            Move computerMove;

            if (computerMode.ToLower() == "random")
            {
                computerMove = gameService.GetComputerMoveRandom(mode);
            }
            else if (computerMode.ToLower() == "lastAction")
            {
                computerMove = gameService.GetComputerMovePrevious(lastPlayerMove);
            }
            else
            {
                throw new ArgumentException("Invalid computer mode specified. Use 'random' or 'lastAction'");
            }

            var outcome = _gameRulesService.GetOutcome(player, computerMove, mode);

            lastPlayerMove = (Move)Enum.Parse(typeof(Move), playerMove, true);

            return Ok(new
            {
                PlayerMove = player,
                ComputerMove = computerMove,
                Outcome = outcome.ToString()
            });
        }

    }
}
