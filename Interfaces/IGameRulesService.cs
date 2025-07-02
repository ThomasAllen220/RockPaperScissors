using RockPaperScissors.Models;

namespace RockPaperScissors.Interfaces
{
    public interface IGameRulesService
    {
        GameOutcome GetOutcome(Move playerMove, Move computerMove, string mode);
    }
}
