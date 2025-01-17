using AutoMapper;
using EnduraGenius.API.Repositories.MuscleRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Controllers;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.AuthRepository;

namespace EnduraGenius.API.Tests.Controller
{
    public class WorkoutControllerTests
    {
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IMapper _mapper;
        private readonly IMuscleRepository _muscleRepository;
        private readonly IAuthRepository _authRepository;

        public WorkoutControllerTests()
        {
            this._workoutsRepository = A.Fake<IWorkoutsRepository>();
            this._mapper = A.Fake<IMapper>();
            this._muscleRepository = A.Fake<IMuscleRepository>();
            this._authRepository = A.Fake<IAuthRepository>();
        }

        [Fact]
        public async Task GetWorkoutById_ShouldReturnsOkResult_whenWorkoutFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns(new Workout());
            // Act
            var result = await controller.GetWorkoutById(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetWorkoutById_ShouldReturnsNotFound_whenWorkoutNotFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns((Workout?)null);
            // Act
            var result = await controller.GetWorkoutById(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetWorkouts_ShouldReturnsOkResult_whenWorkoutsFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkouts(A<string?>._, A<string?>._, A<int>._, A<int>._, A<bool>._)).Returns(new List<Workout>());
            // Act
            var result = await controller.GetWorkouts(null, null);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateWorkout_ShouldReturnsOkResult_whenWorkoutCreated()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _workoutsRepository.CreateWorkout(A<Workout>._, A<Muscle>._, A<Muscle>._, A<string>._)).Returns(new Workout());
            A.CallTo(() => _muscleRepository.GetMuscleByName(A<string>._)).Returns(new Muscle());
            A.CallTo(() => _mapper.Map<GetWorkoutDto>(A<Workout>._)).Returns(new GetWorkoutDto());
            // Act
            var result = await controller.CreateWorkout(new CreateWorkoutRequestDTO());
            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task CreateWorkout_ShouldReturnsUnauthorized_whenUserNotLoggedin()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var result = await controller.CreateWorkout(new CreateWorkoutRequestDTO());
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task CreateWorkout_ShouldReturnsNotFound_whenMainMuscleNotFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _muscleRepository.GetMuscleByName(A<string>._)).Returns((Muscle?)null);
            // Act
            var result = await controller.CreateWorkout(new CreateWorkoutRequestDTO());
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task CreateWorkout_ShouldReturnsBadRequest_whenWorkoutNotCreated()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _muscleRepository.GetMuscleByName(A<string>._)).Returns(new Muscle());
            A.CallTo(() => _workoutsRepository.CreateWorkout(A<Workout>._, A<Muscle>._, A<Muscle>._, A<string>._)).Returns((Workout?)null);
            // Act
            var result = await controller.CreateWorkout(new CreateWorkoutRequestDTO());
            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateWorkout_ShouldReturnsOkResult_whenWorkoutUpdated()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns(new Workout());
            A.CallTo(() => _workoutsRepository.UpdateWorkout(A<Workout>._,A<GetWorkoutDto>._)).Returns(true);
            // Act
            var result = await controller.UpdateWorkout(Guid.NewGuid(), new GetWorkoutDto());
            // Assert
            result.Should().BeOfType<OkResult>();
        }
        [Fact]
        public async Task UpdateWorkout_ShouldReturnsNotFound_whenWorkoutisNotFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns((Workout?)null);
            // Act
            var result = await controller.UpdateWorkout(Guid.NewGuid(), new GetWorkoutDto());
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task UpdateWorkout_ShouldReturnsBadRequest_whenWorkoutNotUpdated()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns(new Workout());
            A.CallTo(() => _workoutsRepository.UpdateWorkout(A<Workout>._, A<GetWorkoutDto>._)).Returns(false);
            // Act
            var result = await controller.UpdateWorkout(Guid.NewGuid(), new GetWorkoutDto());
            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
        [Fact]
        public async Task DeleteWorkout_ShouldReturnsOkResult_whenWorkoutDeleted()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns(new Workout());
            A.CallTo(() => _workoutsRepository.DeleteWorkout(A<Guid>._)).Returns(true);
            // Act
            var result = await controller.DeleteWorkout(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteWorkout_ShouldReturnsNotFound_whenWorkoutNotFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns((Workout?)null);
            // Act
            var result = await controller.DeleteWorkout(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetUncertifiedWorkouts_ShouldReturnOkResult_whenWorkoutsFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.GetWorkouts(null,null,A<int>._, A<int>._, false)).Returns(new List<Workout>());
            // Act
            var result = await controller.GetUncertifiedWorkouts(null,null, 1, 20);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ChangeCertificationStatus_ShouldReturnOkResult_whenWorkoutCertified()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.ChangeCertificationStatus(A<Guid>._)).Returns(new Workout());
            // Act
            var result = await controller.ChangeCertificationStatus(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task ChangeCertificationStatus_ShouldReturnNotFound_whenWorkoutNotFound()
        {
            // Arrange
            var controller = new WorkoutController(_workoutsRepository, _mapper, _muscleRepository, _authRepository);
            A.CallTo(() => _workoutsRepository.ChangeCertificationStatus(A<Guid>._)).Returns((Workout?)null);
            // Act
            var result = await controller.ChangeCertificationStatus(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
