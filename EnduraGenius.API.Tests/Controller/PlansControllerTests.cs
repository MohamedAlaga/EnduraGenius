using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnduraGenius.API.Controllers;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.PlanRepositories;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using AutoMapper;
using Xunit;
using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.Tests.Controllers
{
    public class PlansControllerTests
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IPlansUsersRepository _plansUsersRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IAuthRepository _authRepository;
        private readonly PlansController _controller;

        public PlansControllerTests()
        {
            _planRepository = A.Fake<IPlanRepository>();
            _mapper = A.Fake<IMapper>();
            _planWorkoutsRepository = A.Fake<IPlanWorkoutsRepository>();
            _plansUsersRepository = A.Fake<IPlansUsersRepository>();
            _userWorkoutRepository = A.Fake<IUserWorkoutRepository>();
            _workoutsRepository = A.Fake<IWorkoutsRepository>();
            _authRepository = A.Fake<IAuthRepository>();

            _controller = new PlansController(
                _planRepository,
                _mapper,
                _planWorkoutsRepository,
                _userWorkoutRepository,
                _workoutsRepository,
                _plansUsersRepository,
                _authRepository
            );
        }

        [Fact]
        public async Task GetAllplans_ShouldReturnOk_WhenPlansAreFound()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plans = new List<Plan>();
            A.CallTo(() => _planRepository.GetPublicPlans(userId)).Returns(plans);

            // Act
            var result = await _controller.GetAllplans();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllplans_ShouldReturnUnAuthorized_WhenUserIsNotLoggedin()
        {
            // Arrange
            string? userId = null;
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plans = new List<Plan>();
            A.CallTo(() => _planRepository.GetPublicPlans(userId)).Returns(plans);

            // Act
            var result = await _controller.GetAllplans();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>().Which.StatusCode.Should().Be(401);
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task GetPlanById_ShouldReturnOk_WhenPlanIsFound(Guid planId)
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plan = new Plan();
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, userId)).Returns(plan);
            // Act
            var result = await _controller.GetPlanById(planId);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task GetPlanById_ShouldReturnNotFound_WhenPlanIsNotFound(Guid planId)
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            Plan? plan = null;
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, userId)).Returns(plan);
            // Act
            var result = await _controller.GetPlanById(planId);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task GetPlanById_ShouldReturnUnAuthorized_WhenUserIsNotLoggedin(Guid planId)
        {
            // Arrange
            string? userId = null;
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plan = new Plan();
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, userId)).Returns(plan);
            // Act
            var result = await _controller.GetPlanById(planId);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>().Which.StatusCode.Should().Be(401);
        }

        [Fact]
        public async Task CreatPlan_ShouldReturnOk_WhenPlanIsCreated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newplan = new CreatePlanRequestDTO {Name = "test plan", Descreption = "test plan descreption", workoutsDtos = new List <CreatePlanWorkoutsDto>()};
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plan = new Plan();
            A.CallTo(() => _planRepository.CreatePlan(A<string>._, A<string>._, userId, A<bool>._)).Returns(plan);
            // Act
            var result = await _controller.CreatePlan(newplan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>().Which.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task CreatPlan_ShouldReturnBadRequest_WhenPlanIsNotCreated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newplan = new CreatePlanRequestDTO { Name = "test plan", Descreption = "test plan descreption", workoutsDtos = new List<CreatePlanWorkoutsDto>() };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            Plan? plan = null;
            A.CallTo(() => _planRepository.CreatePlan(A<string>._, A<string>._, userId, A<bool>._)).Returns(plan);
            // Act
            var result = await _controller.CreatePlan(newplan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CreatPlan_ShouldReturnUnAuthorized_WhenUserIsNotLoggedin()
        {
            // Arrange
            string? userId = null;
            var newplan = new CreatePlanRequestDTO { Name = "test plan", Descreption = "test plan descreption", workoutsDtos = new List<CreatePlanWorkoutsDto>() };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plan = new Plan();
            A.CallTo(() => _planRepository.CreatePlan(A<string>._, A<string>._, userId, A<bool>._)).Returns(plan);
            // Act
            var result = await _controller.CreatePlan(newplan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>().Which.StatusCode.Should().Be(401);
        }

        [Fact]
        public async Task CreatPlan_ShouldReturnBadRequest_WhenPlanWorkoutIsNotCreated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            PlanWorkout? planWorkout = null;
            var newplan = new CreatePlanRequestDTO { Name = "test plan", Descreption = "test plan descreption", workoutsDtos = new List<CreatePlanWorkoutsDto> { new CreatePlanWorkoutsDto {DayNumber = 0, Order = 0 , Reps = " 12 * 12 * 12 * 12", WorkoutId = Guid.NewGuid()} } };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plan = new Plan();
            A.CallTo(() => _planRepository.CreatePlan(A<string>._, A<string>._, userId, A<bool>._)).Returns(plan);
            A.CallTo(() => _planWorkoutsRepository.CreatePlanWorkout(A<Plan>._, A<Workout>._,A<string>._ ,A<int>._, A<int>._)).Returns(planWorkout);
            // Act
            var result = await _controller.CreatePlan(newplan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CreatPlan_ShouldReturnBadRequest_WhenWorkoutIsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newplan = new CreatePlanRequestDTO { Name = "test plan", Descreption = "test plan descreption", workoutsDtos = new List<CreatePlanWorkoutsDto> { new CreatePlanWorkoutsDto { DayNumber = 0, Order = 0, Reps = " 12 * 12 * 12 * 12", WorkoutId = Guid.NewGuid() } } };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var plan = new Plan();
            A.CallTo(() => _planRepository.CreatePlan(A<string>._, A<string>._, userId, A<bool>._)).Returns(plan);
            A.CallTo(() => _workoutsRepository.GetWorkoutById(A<Guid>._)).Returns((Workout)null);
            // Act
            var result = await _controller.CreatePlan(newplan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task UpdatePlan_ShouldReturnOk_WhenDataUpdatedSuccessfully(Guid planId)
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var plan = new UpdatePlanRequestDTO { PlanName = "test plan", PlanDescription = "test plan descreption", workoutsDtos = new List<CreatePlanWorkoutsDto>()};
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var planToUpdate = new Plan();
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, userId)).Returns(planToUpdate);
            var updatedPlan = new Plan();
            A.CallTo(() => _planRepository.UpdatePlan(A<Guid>._, userId, A<string>._, A<string>._, A<bool>._, A<List<CreatePlanWorkoutsDto>>._)).Returns(updatedPlan);
            var planWorkouts = new List<PlanWorkout>();
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutByPlanId(A<Guid>._, userId)).Returns(planWorkouts);
            A.CallTo(() => _mapper.Map<PlanResponseDTO>(updatedPlan)).Returns(new PlanResponseDTO());
            A.CallTo(() => _mapper.Map<List<PlanWorkoutsResponseDTO>>(planWorkouts)).Returns(new List<PlanWorkoutsResponseDTO>());
            // Act
            var result = await _controller.UpdatePlan(planId, plan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task UpdatePlan_ShouldReturnNotFound_WhenPlanIsNotFound(Guid planId)
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var plan = new UpdatePlanRequestDTO { PlanName = "test plan", PlanDescription = "test plan descreption", workoutsDtos = new List<CreatePlanWorkoutsDto>() };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            Plan? planToUpdate = null;
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, userId)).Returns(planToUpdate);
            // Act
            var result = await _controller.UpdatePlan(planId, plan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Theory]
        [InlineData("45205575-667d-4543-8c40-fc759ccb44d1")]
        public async Task UpdatePlan_ShouldReturnUnAuthorized_WhenUserIsNotLoggedin(Guid planId)
        {
            // Arrange
            string? userId = null;
            var plan = new UpdatePlanRequestDTO { PlanName = "test plan", PlanDescription = "test plan descreption", workoutsDtos = new List<CreatePlanWorkoutsDto>() };
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var planToUpdate = new Plan();
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, userId)).Returns(planToUpdate);
            // Act
            var result = await _controller.UpdatePlan(planId, plan);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>().Which.StatusCode.Should().Be(401);
        }
    }
}