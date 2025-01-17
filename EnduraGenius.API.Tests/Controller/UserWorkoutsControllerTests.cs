using AutoMapper;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Controllers;
using EnduraGenius.API.Models.DTO;



namespace EnduraGenius.API.Tests.Controller
{
    public class UserWorkoutsControllerTests
    {
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public UserWorkoutsControllerTests()
        {
            _userWorkoutRepository = A.Fake<IUserWorkoutRepository>();
            _mapper = A.Fake<IMapper>();
            _authRepository = A.Fake<IAuthRepository>();
        }

        [Fact]
        public async Task GetUserWorkouts_ShouldReturnsOkResult_whenUserLoggedinAndFoundWorkouts()
        {
            // Arrange
            var controller = new UserWorkoutsController(_userWorkoutRepository, _mapper, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _userWorkoutRepository.GetUserWorkoutByUserId(A<string>._,null,null,1,20)).Returns(new List<UserWorkout>());
            // Act
            var result = await controller.GetUserWorkouts(null,null);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetUserWorkouts_ShouldReturnsUnauthorized_whenUserNotLoggedin()
        {
            // Arrange
            var controller = new UserWorkoutsController(_userWorkoutRepository, _mapper, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var result = await controller.GetUserWorkouts(null, null);
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task UpdateUserWorkout_ShouldReturnsOkResult_whenUserLoggedinAndFoundWorkouts()
        {
            // Arrange
            var controller = new UserWorkoutsController(_userWorkoutRepository, _mapper, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _userWorkoutRepository.UpdateUserWorkout(A<Guid>._, A<string>._, A<float>._, A<float>._, A<int>._)).Returns(new UserWorkout());
            A.CallTo(() => _userWorkoutRepository.GetUserWorkoutByWorkoutId(A<string>._, A<Guid>._)).Returns(new UserWorkout());
            A.CallTo(() => _mapper.Map<UserWorkoutResponseDTO>(A<UserWorkout>._)).Returns(new UserWorkoutResponseDTO());
            // Act
            var result = await controller.UpdateUserWorkout(Guid.NewGuid(), new UpdateUserWorkoutRequestDTO());
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task UpdateUserWorkout_ShouldReturnsUnauthorized_whenUserNotLoggedin()
        {
            // Arrange
            var controller = new UserWorkoutsController(_userWorkoutRepository, _mapper, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var result = await controller.UpdateUserWorkout(Guid.NewGuid(), new UpdateUserWorkoutRequestDTO());
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Fact]
        public async Task UpdateUserWorkout_ShouldReturnsBadRequest_whenUserWorkoutNotUpdated()
        {
            // Arrange
            var controller = new UserWorkoutsController(_userWorkoutRepository, _mapper, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _userWorkoutRepository.UpdateUserWorkout(A<Guid>._, A<string>._, null, null, null)).Returns((UserWorkout?)null);
            // Act
            var result = await controller.UpdateUserWorkout(Guid.NewGuid(), new UpdateUserWorkoutRequestDTO());
            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
