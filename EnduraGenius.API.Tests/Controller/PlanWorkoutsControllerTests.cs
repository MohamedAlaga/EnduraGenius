using AutoMapper;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.PlanRepositories;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using EnduraGenius.API.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Tests.Controller
{
    public class PlanWorkoutsControllerTests
    {
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public PlanWorkoutsControllerTests()
        {

            this._planWorkoutsRepository = A.Fake<IPlanWorkoutsRepository>();
            this._planRepository = A.Fake<IPlanRepository>();
            this._workoutsRepository = A.Fake<IWorkoutsRepository>();
            this._userWorkoutRepository = A.Fake<IUserWorkoutRepository>();
            this._mapper = A.Fake<IMapper>();
            this._authRepository = A.Fake<IAuthRepository>();

        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task UpdatePlanWorkout_ShouldReturnOk_WhenDoneSuccefully(Guid id)
        {
            // Arrange
            var newWorkout = new UpdatePlanWorkoutRequestDTO
            {
                NewWorkoutId = Guid.NewGuid(),
                NewReps = "10",
                NewDayNumber = 1,
                NewOrder = 1
            };
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planWorkoutsRepository.UpdatePlanWorkout(A<Guid>._, A<string>._, A<Guid>._, A<string>._, A<int>._, A<int>._)).Returns(new PlanWorkout());
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutById(A<Guid>._, A<string>._)).Returns(new PlanWorkout());
            A.CallTo(() => _mapper.Map<PlanWorkoutsResponseDTO>(A<PlanWorkout>._)).Returns(new PlanWorkoutsResponseDTO());
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.UpdatePlanWorkout(id, newWorkout);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task UpdatePlanWorkout_ShouldReturnBadRequest_WhenUpdatePlanWorkoutReturnsNull(Guid id)
        {
            // Arrange
            var newWorkout = new UpdatePlanWorkoutRequestDTO
            {
                NewWorkoutId = Guid.NewGuid(),
                NewReps = "10",
                NewDayNumber = 1,
                NewOrder = 1
            };
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planWorkoutsRepository.UpdatePlanWorkout(A<Guid>._, A<string>._, A<Guid>._, A<string>._, A<int>._, A<int>._)).Returns((PlanWorkout)null);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.UpdatePlanWorkout(id, newWorkout);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task UpdatePlanWorkout_ShouldReturnUnauthorized_WhenUserNotSignedIn(Guid id)
        {
            // Arrange
            var newWorkout = new UpdatePlanWorkoutRequestDTO
            {
                NewWorkoutId = Guid.NewGuid(),
                NewReps = "10",
                NewDayNumber = 1,
                NewOrder = 1
            };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.UpdatePlanWorkout(id, newWorkout);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task GetPlanWorkoutById_ShouldReturnOk_WhenDoneSuccefully(Guid id)
        {
            // Arrange
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutById(A<Guid>._, A<string>._)).Returns(new PlanWorkout());
            A.CallTo(() => _mapper.Map<PlanWorkoutsResponseDTO>(A<PlanWorkout>._)).Returns(new PlanWorkoutsResponseDTO());
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.GetPlanWorkoutById(id);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task GetPlanWorkoutById_ShouldReturnNotFound_WhenPlanWorkoutIsNull(Guid id)
        {
            // Arrange
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutById(A<Guid>._, A<string>._)).Returns((PlanWorkout)null);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.GetPlanWorkoutById(id);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task GetPlanWorkoutById_ShouldReturnUnauthorized_WhenUserNotSignedIn(Guid id)
        {
            // Arrange
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.GetPlanWorkoutById(id);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task CreatePlanWorkout_ShouldReturnOk_WhenDoneSuccefully()
        {
            // Arrange
            var planWorkout = new CreatePlanWorkoutRequestDTO
            {
                Plan = Guid.NewGuid(),
                Workout = Guid.NewGuid(),
                Reps = "10",
                DayNumber = 1,
                Order = 1
            };
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planWorkoutsRepository.CreatePlanWorkout(A<Plan>._,A<Workout>._,A<string>._,A<int>._,A<int>._)).Returns(new PlanWorkout());
            A.CallTo(() => _mapper.Map<PlanWorkoutsResponseDTO>(A<PlanWorkout>._)).Returns(new PlanWorkoutsResponseDTO());
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.CreatePlanWorkout(planWorkout);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task CreatePlanWorkout_ShouldReturnBadRequest_WhenPlanIsNull()
        {
            // Arrange
            var planWorkout = new CreatePlanWorkoutRequestDTO
            {
                Plan = Guid.NewGuid(),
                Workout = Guid.NewGuid(),
                Reps = "10",
                DayNumber = 1,
                Order = 1
            };
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, A<string>._)).Returns((Plan)null);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.CreatePlanWorkout(planWorkout);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreatePlanWorkout_ShouldReturnBadRequest_WhenWorkoutIsNull()
        {
            // Arrange
            var planWorkout = new CreatePlanWorkoutRequestDTO
            {
                Plan = Guid.NewGuid(),
                Workout = Guid.NewGuid(),
                Reps = "10",
                DayNumber = 1,
                Order = 1
            };
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, A<string>._)).Returns(new Plan());
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns((Workout)null);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.CreatePlanWorkout(planWorkout);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreatePlanWorkout_shouldReturnUnAuthorized_WhenuserIsnotSignedIn()
        {
            //Arrange
            var planWorkout = new CreatePlanWorkoutRequestDTO
            {
                Plan = Guid.NewGuid(),
                Workout = Guid.NewGuid(),
                Reps = "10",
                DayNumber = 1,
                Order = 1
            };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            //Act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.CreatePlanWorkout(planWorkout);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task DeletePlanWorkout_ShouldReturnOk_WhenDoneSuccefully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planWorkoutsRepository.DeletePlanWorkout(A<Guid>._, A<string>._)).Returns(true);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.DeletePlanWorkout(id);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeletePlanWorkout_ShouldReturnNotFound_WhenDeletePlanWorkoutReturnsFalse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userId = "6007cbfa-fb89-4cb1-91cd-4dc60ae36d40";
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _planWorkoutsRepository.DeletePlanWorkout(A<Guid>._, A<string>._)).Returns(false);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.DeletePlanWorkout(id);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeletePlanWorkout_ShouldReturnUnauthorized_WhenUserNotSignedIn()
        {
            // Arrange
            var id = Guid.NewGuid();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // act
            var controller = new PlanWorkoutsController(_planWorkoutsRepository, _planRepository, _workoutsRepository, _mapper, _userWorkoutRepository, _authRepository);
            var result = await controller.DeletePlanWorkout(id);
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
