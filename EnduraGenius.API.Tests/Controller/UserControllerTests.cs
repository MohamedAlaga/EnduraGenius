using AutoMapper;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.UserRepository;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EnduraGenius.API.Tests.Controller
{
    public class UserControllerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _userWorkoutRepository = A.Fake<IUserWorkoutRepository>();
            _mapper = A.Fake<IMapper>();
            _authRepository = A.Fake<IAuthRepository>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
        }

        [Fact]
        public async Task GetUser_ShouldReturnOkObjectResult_WhenUserExistsAndLoggedIn()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var user = new User();
            A.CallTo(() => _userRepository.GetUserById(userId)).Returns(user);
            var userWorkouts = new List<UserWorkout>();
            A.CallTo(() => _userWorkoutRepository.GetUserWorkoutByUserId(userId, null, null,A<int>._, A<int>._)).Returns(userWorkouts);
            var userDTO = new UserProfileResponseDTO();
            A.CallTo(() => _mapper.Map<UserProfileResponseDTO>(user)).Returns(userDTO);
            var userWorkoutDTO = new List<UserWorkoutResponseDTO>();
            A.CallTo(() => _mapper.Map<List<UserWorkoutResponseDTO>>(userWorkouts)).Returns(userWorkoutDTO);
            // Act
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            var result = await controller.GetUser(null,null);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task GetUser_ShouldReturnUnauthorized_WhenUserIsNotLoggedIn()
        {
            // Arrange
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            var result = await controller.GetUser(null, null);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Fact]
        public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _userRepository.GetUserById(userId)).Returns((User?)null);
            // Act
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            var result = await controller.GetUser(null, null);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task UpdateUserPoints_ShouldReturnOkObjectResult_WhenUserExistsAndLoggedIn()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var user = new User();
            A.CallTo(() => _userRepository.EditUserPoints(userId, A<int>._)).Returns(user);
            var newPoints = new EditPointsDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.UpdateUserPoints(newPoints);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task UpdateUserPoints_ShouldReturnUnauthorized_WhenUserIsNotLoggedIn()
        {
            // Arrange
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            var newPoints = new EditPointsDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.UpdateUserPoints(newPoints);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Fact]
        public async Task UpdateUserPoints_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _userRepository.EditUserPoints(userId, A<int>._)).Returns((User?)null);
            var newPoints = new EditPointsDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.UpdateUserPoints(newPoints);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task AddUserPoints_ShouldReturnOkObjectResult_WhenUserExistsAndLoggedIn()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var user = new User();
            A.CallTo(() => _userRepository.AddUserPoints(userId, A<int>._)).Returns(user);
            var newPoints = new EditPointsDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.AddUserPoints(newPoints);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task AddUserPoints_ShouldReturnUnauthorized_WhenUserIsNotLoggedIn()
        {
            // Arrange
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            var newPoints = new EditPointsDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.AddUserPoints(newPoints);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Fact]
        public async Task AddUserPoints_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _userRepository.AddUserPoints(userId, A<int>._)).Returns((User?)null);
            var newPoints = new EditPointsDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.AddUserPoints(newPoints);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task updateUserBody_ShouldReturnOkObjectResult_WhenUserExistsAndLoggedIn()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var user = new User();
            A.CallTo(() => _userRepository.GetUserById(userId)).Returns(user);
            var updateUserBodyDTO = new UpdateUserBodyDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.updateUserBody(updateUserBodyDTO);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task updateUserBody_ShouldReturnUnauthorized_WhenUserIsNotLoggedIn()
        {
            // Arrange
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            var updateUserBodyDTO = new UpdateUserBodyDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.updateUserBody(updateUserBodyDTO);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Fact]
        public async Task updateUserBody_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _userRepository.EditUserBodyData(A<string>._,null,null,null,null,null)).Returns((User?)null);
            var updateUserBodyDTO = new UpdateUserBodyDTO();
            var controller = new UserController(_userRepository, _mapper, _userWorkoutRepository, _authRepository, _httpContextAccessor);
            // Act
            var result = await controller.updateUserBody(updateUserBodyDTO);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
