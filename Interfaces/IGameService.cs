using RockPaperScissors.Models;

namespace RockPaperScissors.Interfaces
{
    public interface IGameService
    {
        Move GetComputerMoveRandom(string mode);
        Move GetComputerMovePrevious(Move lastPlayerMove);
    }
}
