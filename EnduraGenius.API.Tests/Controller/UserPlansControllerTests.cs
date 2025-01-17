using AutoMapper;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.PlanRepositories;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Tests.Controller
{
    public class UserPlansControllerTests
    {
        private readonly IPlansUsersRepository _plansUsersRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IAuthRepository _authRepository;
        public UserPlansControllerTests()
        {
            _plansUsersRepository = A.Fake<IPlansUsersRepository>();
            _planRepository = A.Fake<IPlanRepository>();
            _mapper = A.Fake<IMapper>();
            _planWorkoutsRepository = A.Fake<IPlanWorkoutsRepository>();
            _authRepository = A.Fake<IAuthRepository>();
        }

        [Fact]
        public async Task GetUserPlans_ShouldReturnsOkResult_whenUserLoggedinAndFoundPlans()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _plansUsersRepository.GetPlansUserByUserId(A<string>._)).Returns(new List<PlansUsers>());
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, A<string>._)).Returns(new Plan());
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutByPlanId(A<Guid>._, A<string>._)).Returns(new List<PlanWorkout>());
            // Act
            var result = await controller.GetUserPlans();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetUserPlans_ShouldReturnsUnauthorized_whenUserNotLoggedin()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var result = await controller.GetUserPlans();
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task GetCurrentPlan_ShouldReturnsOkResult_whenUserLoggedinAndFoundPlans()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _plansUsersRepository.GetPlansUserByUserId(A<string>._)).Returns(new List<PlansUsers>());
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, A<string>._)).Returns(new Plan());
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutByPlanId(A<Guid>._, A<string>._)).Returns(new List<PlanWorkout>());
            // Act
            var result = await controller.GetCurrentPlan();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetCurrentPlan_ShouldReturnsUnauthorized_whenUserNotLoggedin()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var result = await controller.GetCurrentPlan();
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task SetCurrentPlan_ShouldReturnOkResult_whenUserLoggedinAndPlanFound()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _plansUsersRepository.GetPlansUserByUserId(A<string>._)).Returns(new List<PlansUsers>());
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, A<string>._)).Returns(new Plan());
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutByPlanId(A<Guid>._, A<string>._)).Returns(new List<PlanWorkout>());
            // Act
            var result = await controller.SetCurrentPlan(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task SetCurrentPlan_ShouldReturnUnauthorized_whenUserNotLoggedin()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var result = await controller.SetCurrentPlan(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        public async Task UnsubscribeFromPlan_ShouldReturnOkResult_whenUserLoggedinAndPlanFound()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _plansUsersRepository.GetPlansUserByUserId(A<string>._)).Returns(new List<PlansUsers>());
            A.CallTo(() => _planRepository.GetPlanById(A<Guid>._, A<string>._)).Returns(new Plan());
            A.CallTo(() => _planWorkoutsRepository.GetPlanWorkoutByPlanId(A<Guid>._, A<string>._)).Returns(new List<PlanWorkout>());
            // Act
            var result = await controller.UnsubscribeFromPlan(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task UnsubscribeFromPlan_ShouldReturnUnauthorized_whenUserNotLoggedin()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(null);
            // Act
            var result = await controller.UnsubscribeFromPlan(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task UnsubscribeFromPlan_ShouldReturnNotFound_whenUserLoggedinAndPlanNotFound()
        {
            // Arrange
            var controller = new UserPlansController(_plansUsersRepository, _mapper, _planRepository, _planWorkoutsRepository, _authRepository);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(Guid.NewGuid().ToString());
            A.CallTo(() => _plansUsersRepository.UnsubscibeUserFromPlan(A<string>._,A<Guid>._)).Returns(false);
            // Act
            var result = await controller.UnsubscribeFromPlan(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
