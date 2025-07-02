namespace RockPaperScissors.Models
{
    public class GameRulesModel
    {
        public Dictionary<Move, Move[]> WinsAgainst { get; } = new()
        {
            {Move.Rock, new[] {Move.Scissors, Move.Lizard}},
            {Move.Paper, new[] {Move.Rock, Move.Spock}},
            {Move.Scissors, new[] {Move.Paper, Move.Lizard}},
            {Move.Lizard, new[] {Move.Paper, Move.Spock}},
            {Move.Spock, new[] {Move.Rock, Move.Scissors}}
        };
    }
}
