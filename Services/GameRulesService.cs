using RockPaperScissors.Models;
using RockPaperScissors.Interfaces;

namespace RockPaperScissors.Services
{
    public class GameRulesService : IGameRulesService
    {
        private readonly GameRulesModel _rules;

        public GameRulesService(GameRulesModel rules)
        {
            _rules = rules;
        }

        public GameOutcome GetOutcome(Move playerMove, Move computerMove, string mode)
        {
            // Optional: restrict logic if classic mode is selected
            if (mode.ToLower() == "classic" && (int)playerMove > 2 || (int)computerMove > 2)
            {
                throw new ArgumentException("Invalid move for classic mode.");
            }

            if (playerMove == computerMove) return GameOutcome.Draw;

            if (_rules.WinsAgainst[playerMove].Contains(computerMove))
            {
                return GameOutcome.Win;
            }

            return GameOutcome.Lose;
        }
    }
}
