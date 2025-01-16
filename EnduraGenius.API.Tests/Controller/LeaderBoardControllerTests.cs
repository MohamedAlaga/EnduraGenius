using EnduraGenius.API.Repositories.UserRepository;
using FakeItEasy;
using EnduraGenius.API.Controllers;
using EnduraGenius.API.Models.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;


namespace EnduraGenius.API.Tests.Controller
{
    public class LeaderBoardControllerTests
    {
        private readonly IUserRepository _userRepository;
        public LeaderBoardControllerTests()
        {
            this._userRepository = A.Fake<IUserRepository>();
        }

        [Fact]
        public async Task getUsersLeaderBoard_WhenCalled_ReturnsLeaderBoard()
        {
            // Arrange
            var leaderBoardController = new LeaderBoardController(this._userRepository);
            var leaderBoard = new List<LeaderBoardResponseDTO>
            {
                new LeaderBoardResponseDTO
                {
                    Points = 200,
                    UserName = "Test",
                },
                new LeaderBoardResponseDTO
                {
                    Points = 100,
                    UserName = "Test2",
                }
            };
            A.CallTo(() => this._userRepository.LeaderBoard()).Returns(leaderBoard);
            // Act
            var result = await leaderBoardController.getUsersLeaderBoard();
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(leaderBoard);
        }
    }
}
