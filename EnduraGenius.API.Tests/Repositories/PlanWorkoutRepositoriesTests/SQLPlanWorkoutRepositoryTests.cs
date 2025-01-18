
using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;


namespace EnduraGenius.API.Tests.Repositories.PlansUsersRepositoriesTests
{
    public class SQLPlanWorkoutRepositoryTests
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        public SQLPlanWorkoutRepositoryTests()
        {
            _dbcontext = new EnduraGeniusTestingDBContexts().GetDBContextWithData().Result;
        }

        [Fact]
        public async Task CreatePlanWorkout_ShouldReturnPlanWorkout_WhenCreated()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var plan = await _dbcontext.Plans.FirstOrDefaultAsync();
            var workout = await _dbcontext.Workouts.FirstOrDefaultAsync();
            string reps = "10";
            int dayNumber = 1;
            int order = 1;
            //act
            var planWorkoutsCount = await _dbcontext.PlanWorkouts.CountAsync();
            var result = await planWorkoutRepository.CreatePlanWorkout(plan, workout, reps, dayNumber, order);
            var newPlanWorkoutsCount = await _dbcontext.PlanWorkouts.CountAsync();
            //assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.PlanId.Should().Be(plan.Id);
            result.WorkoutId.Should().Be(workout.Id);
            result.Reps.Should().Be(reps);
            result.DayNumber.Should().Be(dayNumber);
            result.Order.Should().Be(order);
            newPlanWorkoutsCount.Should().Be(planWorkoutsCount + 1);
        }

        [Fact]
        public async Task DeletePlanWorkout_ShouldReturnTrue_WhenDeleted()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planWorkoutId = Guid.Parse("A46BD22D-952A-4CBD-DF2B-08DD31E3B9B3");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var planWorkoutsCount = await _dbcontext.PlanWorkouts.CountAsync();
            var result = await planWorkoutRepository.DeletePlanWorkout(planWorkoutId, userId);
            var newPlanWorkoutsCount = await _dbcontext.PlanWorkouts.CountAsync();
            //assert
            result.Should().BeTrue();
            newPlanWorkoutsCount.Should().Be(planWorkoutsCount - 1);
        }

        [Fact]
        public async Task DeletePlanWorkout_ShouldReturnFalse_WhenNotDeleted()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planWorkoutId = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0C");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            //act
            var planWorkoutsCount = await _dbcontext.PlanWorkouts.CountAsync();
            var result = await planWorkoutRepository.DeletePlanWorkout(planWorkoutId, userId);
            var newPlanWorkoutsCount = await _dbcontext.PlanWorkouts.CountAsync();
            //assert
            result.Should().BeFalse();
            newPlanWorkoutsCount.Should().Be(planWorkoutsCount);
        }
        [Fact]
        public async Task GetPlanWorkoutByPlanId_ShouldReturnPlanWorkout_WhenFound()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planWorkoutRepository.GetPlanWorkoutByPlanId(planId, userId);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetPlanWorkoutByPlanId_ShouldReturnEmptyList_WhenNotFound()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99D");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planWorkoutRepository.GetPlanWorkoutByPlanId(planId, userId);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }
        [Fact]
        public async Task GetPlanWorkoutById_ShouldReturnPlanWorkout_WhenFound()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planWorkoutId = Guid.Parse("A46BD22D-952A-4CBD-DF2B-08DD31E3B9B3");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planWorkoutRepository.GetPlanWorkoutById(planWorkoutId, userId);
            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(planWorkoutId);
        }
        [Fact]
        public async Task GetPlanWorkoutById_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planWorkoutId = Guid.Parse("A46BD22D-952A-4CBD-DF2B-08DD31E3B9B4");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planWorkoutRepository.GetPlanWorkoutById(planWorkoutId, userId);
            //assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task UpdatePlanWorkout_ShouldReturnPlanWorkout_WhenUpdated()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planWorkoutId = Guid.Parse("A46BD22D-952A-4CBD-DF2B-08DD31E3B9B3");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            var newWorkoutId = Guid.Parse("FDD60EE0-3C7F-4E98-FBC5-08DD3151CB88");
            string reps = "10";
            int dayNumber = 1;
            int order = 1;
            //act
            var result = await planWorkoutRepository.UpdatePlanWorkout(planWorkoutId, userId, newWorkoutId, reps, dayNumber, order);
            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(planWorkoutId);
            result.WorkoutId.Should().Be(newWorkoutId);
            result.Reps.Should().Be(reps);
            result.DayNumber.Should().Be(dayNumber);
            result.Order.Should().Be(order);
        }
        [Fact]
        public async Task UpdatePlanWorkout_ShouldReturnNull_WhenNotUpdated()
        {
            //arrange
            var planWorkoutRepository = new SQLPlanWorkoutRepository(_dbcontext);
            var planWorkoutId = Guid.Parse("A46BD22D-952A-4CBD-DF2B-08DD31E3B9B4");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            var newWorkoutId = Guid.Parse("FDD60EE0-3C7F-4E98-FBC5-08DD3151CB88");
            string reps = "10";
            int dayNumber = 1;
            int order = 1;
            //act
            var result = await planWorkoutRepository.UpdatePlanWorkout(planWorkoutId, userId, newWorkoutId, reps, dayNumber, order);
            //assert
            result.Should().BeNull();
        }
    }
}
