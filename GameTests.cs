using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RockPaperScissors.Models;
using RockPaperScissors.Services;
using Xunit;

namespace RockPaperScissors.Tests
{
    public class GameControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GameControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("Rock", "Scissors", "Win")]
        [InlineData("Paper", "Rock", "Win")]
        [InlineData("Scissors", "Paper", "Win")]
        [InlineData("Rock", "Paper", "Lose")]
        [InlineData("Rock", "Rock", "Draw")]
        public async Task PlayClassicGame_ReturnsExpectedResult(string playerMove, string computerMove, string expectedOutcome)
        {
            var response = await _client.GetAsync($"/api/game/play?playerMove={playerMove}&computerMove={computerMove}&mode=classic");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains(expectedOutcome, content, System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task PlayClassicGame_InvalidMove_ReturnsBadRequest()
        {
            var response = await _client.GetAsync("/api/game/play?playerMove=Banana&computerMove=Rock&mode=classic");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

    public class GameControllerExtendedTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GameControllerExtendedTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("Spock", "Scissors", "Win")]
        [InlineData("Spock", "Rock", "Win")]
        [InlineData("Lizard", "Paper", "Win")]
        [InlineData("Spock", "Paper", "Lose")]
        [InlineData("Spock", "Spock", "Draw")]
        public async Task PlayExtendedGame_ReturnsExpectedResult(string playerMove, string computerMove, string expectedOutcome)
        {
            var response = await _client.GetAsync($"/api/game/play?playerMove={playerMove}&computerMove={computerMove}&mode=extended");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains(expectedOutcome, content, System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task PlayExtendedGame_InvalidMove_ReturnsBadRequest()
        {
            var response = await _client.GetAsync("/api/game/play?playerMove=Banana&computerMove=Rock&mode=extended");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

    public class ComputerPlayerTests
    {
        [Fact]
        public void RandomPlayer_OnlyGeneratesValidClassicMoves()
        {
            var gameService = new GameService(); // Make sure this exists and implements the correct interface
            var validMoves = new[] { Move.Rock, Move.Paper, Move.Scissors };

            for (int i = 0; i < 100; i++)
            {
                var move = gameService.GetComputerMoveRandom("classic");
                Assert.Contains(move, validMoves);
            }
        }

        [Fact]
        public void LastChoicePlayer_RepeatsLastUserMove_Classic()
        {
            var gameService = new GameService();
            var userLastMove = Move.Paper;

            var move = gameService.GetComputerMovePrevious(userLastMove);

            Assert.Equal(userLastMove, move);
        }
    }

    public class ComputerPlayerExtendedTests
    {
        [Fact]
        public void RandomPlayer_OnlyGeneratesValidExtendedMoves()
        {
            var gameService = new GameService();
            var validMoves = new[] { Move.Rock, Move.Paper, Move.Scissors, Move.Lizard, Move.Spock };

            for (int i = 0; i < 100; i++)
            {
                var move = gameService.GetComputerMoveRandom("extended");
                Assert.Contains(move, validMoves);
            }
        }

        [Fact]
        public void LastChoicePlayer_RepeatsLastUserMove_Extended()
        {
            var gameService = new GameService();
            var userLastMove = Move.Lizard;

            var move = gameService.GetComputerMovePrevious(userLastMove);

            Assert.Equal(userLastMove, move);
        }
    }
}