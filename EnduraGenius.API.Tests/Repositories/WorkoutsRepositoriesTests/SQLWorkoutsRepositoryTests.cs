using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using EnduraGenius.API.Repositories;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;


namespace EnduraGenius.API.Tests.Repositories.WorkoutsRepositoriesTests
{
    public class SQLWorkoutsRepositoryTests
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        public SQLWorkoutsRepositoryTests()
        {
            _dbcontext = new EnduraGeniusTestingDBContexts().GetDBContextWithData().Result;
        }

        [Fact]
        public async Task CreateWorkout_ShouldReturnWorkout_WhenCreated()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            var muscle = await _dbcontext.Muscles.FirstOrDefaultAsync();
            var workout = new Workout
            {
                Name = "Test Workout",
                Description = "Test Description",
                WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
                CreatedAt = DateTime.Now,
                IsCertified = true,
                Link = "https://www.youtube.com/watch?v=9bZkp7q19f0",
                UpdatedAt = DateTime.Now,
                MainMuscleId = muscle.Id,
                SecondaryMuscleId = muscle.Id,
            };
            //act
            var result = await workoutRepository.CreateWorkout(workout, muscle, muscle, "29b0975c-b32f-4842-988a-e038f0470fde");
            //assert
            result.Should().NotBeNull();
            result.MainMuscle.Should().Be(muscle);
            result.SecondaryMuscle.Should().Be(muscle);
            result.WorkoutCreatedBy.Should().Be("29b0975c-b32f-4842-988a-e038f0470fde");
            result.IsCertified.Should().BeTrue();
            result.Name.Should().Be("Test Workout");
            result.Description.Should().Be("Test Description");
            result.Link.Should().Be("https://www.youtube.com/watch?v=9bZkp7q19f0");
        }

        [Fact]
        public async Task DeleteWorkout_ShouldReturnTrue_WhenDeleted()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            var workout = await _dbcontext.Workouts.FirstOrDefaultAsync();
            //act
            var workoutsCount = await _dbcontext.Workouts.CountAsync();
            var result = await workoutRepository.DeleteWorkout(workout.Id);
            var newWorkoutsCount = await _dbcontext.Workouts.CountAsync();
            //assert
            result.Should().BeTrue();
            newWorkoutsCount.Should().Be(workoutsCount - 1);
        }
        [Fact]
        public async Task DeleteWorkout_ShouldReturnFalse_WhenNotDeleted()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            //act
            var workoutsCount = await _dbcontext.Workouts.CountAsync();
            var result = await workoutRepository.DeleteWorkout(Guid.NewGuid());
            var newWorkoutsCount = await _dbcontext.Workouts.CountAsync();
            //assert
            result.Should().BeFalse();
            newWorkoutsCount.Should().Be(workoutsCount);
        }
        [Fact]
        public async Task GetWorkoutById_ShouldReturnWorkout_WhenExists()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            var workout = await _dbcontext.Workouts.FirstOrDefaultAsync();
            //act
            var result = await workoutRepository.GetWorkoutById(workout.Id);
            //assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(workout.Id);
        }

        [Fact]
        public async Task GetWorkoutById_ShouldReturnNull_WhenNotExists()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            //act
            var result = await workoutRepository.GetWorkoutById(Guid.NewGuid());
            //assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task GetWorkouts_ShouldReturnWorkouts_WhenExists()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            //act
            var result = await workoutRepository.GetWorkouts(null, null, 1, 10, true);
            //assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public async Task GetWorkouts_ShouldReturnEmptyList_WhenNotExists()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            //act
            var result = await workoutRepository.GetWorkouts(null, null, 1, 10, false);
            //assert
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task UpdateWorkout_ShouldReturnTrue_WhenUpdated()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            var workout = await _dbcontext.Workouts.FirstOrDefaultAsync();
            var updateWorkoutDto = new GetWorkoutDto
            {
                Name = "Updated Name",
                Link = "https://www.youtube.com/watch?v=9bZkp7q19f0",
                Description = "Updated Description",
                MainMuscle = "Biceps"
            };
            //act
            var result = await workoutRepository.UpdateWorkout(workout, updateWorkoutDto);
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateWorkout_ShouldReturnFalse_WhenNotUpdated()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            var workout = await _dbcontext.Workouts.FirstOrDefaultAsync();
            var updateWorkoutDto = new GetWorkoutDto
            {
                Name = "Updated Name",
                Link = "https://www.youtube.com/watch?v=9bZkp7q19f0",
                Description = "Updated Description",
                MainMuscle = "test"
            };
            //act
            var result = await workoutRepository.UpdateWorkout(workout, updateWorkoutDto);
            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ChangeCertificationStatus_ShouldReturnWorkout_WhenChanged()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            var workout = await _dbcontext.Workouts.FirstOrDefaultAsync();
            //act
            var isCertified = workout.IsCertified;
            var result = await workoutRepository.ChangeCertificationStatus(workout.Id);
            //assert
            result.Should().NotBeNull();
            result!.IsCertified.Should().Be(!isCertified);
        }

        [Fact]
        public async Task ChangeCertificationStatus_ShouldReturnNull_WhenNotChanged()
        {
            //arrange
            var workoutRepository = new SQLWorkoutsRepository(_dbcontext);
            //act
            var result = await workoutRepository.ChangeCertificationStatus(Guid.NewGuid());
            //assert
            result.Should().BeNull();
        }

    }
}
