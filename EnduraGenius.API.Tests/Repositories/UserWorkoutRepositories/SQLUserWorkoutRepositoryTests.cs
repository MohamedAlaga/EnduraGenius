using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using AutoMapper;
using FakeItEasy;
namespace EnduraGenius.API.Tests.Repositories.UserWorkoutRepositories
{
    public class SQLUserWorkoutRepositoryTests
    {
        private readonly EnduraGeniusDBContext _dbcontext;

        public SQLUserWorkoutRepositoryTests()
        {
            _dbcontext = new EnduraGeniusTestingDBContexts().GetDBContextWithData().Result;
        }

        [Fact]
        public async Task CreateUserWorkout_ShouldReturnUserWorkout_WhenCreated()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var workout = await _dbcontext.Workouts.FirstOrDefaultAsync();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.CreateUserWorkout(workout, userId);
            //assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
            result.WorkoutId.Should().Be(workout.Id);
        }
        [Fact]
        public async Task CreateUserWorkout_ShouldReturnNull_WhenAlreadyExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var workout = await _dbcontext.Workouts.FindAsync(Guid.Parse("AEE71966-6DF2-4806-FBBF-08DD3151CB88"));
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.CreateUserWorkout(workout!, userId);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteUserWorkout_ShouldReturnTrue_WhenDeleted()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.DeleteUserWorkout(userWorkout.Id, userId);
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteUserWorkout_ShouldReturnFalse_WhenNotDeleted()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.DeleteUserWorkout(Guid.NewGuid(), userId);
            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetUserWorkoutById_ShouldReturnUserWorkout_WhenExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.GetUserWorkoutById(userWorkout.Id, userId);
            //assert
            result.Should().NotBeNull();
            result!.UserId.Should().Be(userId);
        }
        [Fact]
        public async Task GetUserWorkoutById_ShouldReturnNull_WhenNotExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.GetUserWorkoutById(Guid.NewGuid(), userId);
            //assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task GetUserWorkoutByUserId_ShouldReturnUserWorkouts_WhenExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.GetUserWorkoutByUserId(userId, null, null, 1, 10);
            //assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task GetUserWorkoutByUserId_ShouldReturnEmptyList_WhenNotExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userId = Guid.NewGuid().ToString();
            //act
            var result = await userWorkoutRepository.GetUserWorkoutByUserId(userId, null, null, 1, 10);
            //assert
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetUserWorkoutByWorkoutId_ShouldReturnUserWorkout_WhenExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.GetUserWorkoutByWorkoutId(userId, userWorkout.WorkoutId);
            //assert
            result.Should().NotBeNull();
            result!.UserId.Should().Be(userId);
        }
        [Fact]
        public async Task GetUserWorkoutByWorkoutId_ShouldReturnNull_WhenNotExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.GetUserWorkoutByWorkoutId(userId, Guid.NewGuid());
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateUserWorkout_ShouldReturnUserWorkout_WhenUpdated()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = userWorkout!.UserId;
            //act
            var result = await userWorkoutRepository.UpdateUserWorkout(userWorkout!.WorkoutId, userId, 10, 5,5);
            //assert
            result.Should().NotBeNull();
            result!.MaxWeight.Should().Be(10);
            result.LastWeight.Should().Be(5);
        }

        [Fact]
        public async Task UpdateUserWorkout_ShouldReturnNull_WhenNotExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.UpdateUserWorkout(Guid.NewGuid(), userId, 10, 5, 5);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateUserWorkout_ShouldReturnNull_WhenNotAuthorized()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fdd";
            //act
            var result = await userWorkoutRepository.UpdateUserWorkout(userWorkout!.WorkoutId, userId, 10, 5, 5);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddOneTimesPerformed_ShouldReturnTrue_WhenAdded()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = userWorkout!.UserId;
            //act
            var result = await userWorkoutRepository.AddOneTimesPerformed(userId, userWorkout.WorkoutId);
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddOneTimesPerformed_ShouldReturnFalse_WhenNotExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.AddOneTimesPerformed(userId, Guid.NewGuid());
            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task RemoveOneTimesPerformed_ShouldReturnTrue_WhenRemoved()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userWorkout = await _dbcontext.UserWorkouts.FirstOrDefaultAsync();
            var userId = userWorkout!.UserId;
            //act
            var result = await userWorkoutRepository.RemoveOneTimesPerformed(userId, userWorkout.WorkoutId);
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveOneTimesPerformed_ShouldReturnFalse_WhenNotExists()
        {
            //arrange
            var userWorkoutRepository = new SQLUserWorkoutRepository(_dbcontext);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userWorkoutRepository.RemoveOneTimesPerformed(userId, Guid.NewGuid());
            //assert
            result.Should().BeFalse();
        }
    }
}
