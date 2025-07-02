using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;

namespace RockPaperScissors.Services
{
    public class GameService : IGameService
    {
        private readonly Random random = new();

        public Move GetComputerMoveRandom(string mode)
        {
            int moveCount;
            if (mode.ToLower() == "classic") { moveCount = 3; }
            else if (mode.ToLower() == "extended") { moveCount = Enum.GetValues(typeof(Move)).Length; }
            else
            {
                throw new ArgumentException("Invalid mode specified. Use 'classic' or 'extended'");
             }


            var allMoves = Enum.GetValues(typeof(Move));
            var randomIndex = random.Next(moveCount);
            return (Move)allMoves.GetValue(randomIndex);
        }
        public Move GetComputerMovePrevious(Move lastPlayerMove)
        {
            return lastPlayerMove;
        }
    }
}
