
using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using EnduraGenius.API.Repositories;

namespace EnduraGenius.API.Tests.Repositories.MuscleRepositories
{
    public class SQLMuscleRepositoryTests
    {
        private readonly EnduraGeniusDBContext _context;
        public SQLMuscleRepositoryTests()
        {
            var db = new EnduraGeniusTestingDBContexts();
            _context = db.GetDBContextWithData().Result;
        }

        [Fact]
        public async Task CreateMuscle_ShouldReturnMuscle_WhenCreated()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscle = new Muscle
            {
                Name = "Test Muscle",
                Description = "Test Description"
            };
            //act
            var musclesCount = await _context.Muscles.CountAsync();
            var result = await muscleRepository.CreateMuscle(muscle);
            var newMuscelsCount = await _context.Muscles.CountAsync();
            //assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Name.Should().Be(muscle.Name);
            result.Description.Should().Be(muscle.Description);
            newMuscelsCount.Should().Be(musclesCount + 1);
        }
        [Fact]
        public async Task CreateMuscle_ShouldReturnNull_WhenNotCreated()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscle = new Muscle
            {
                Name = "Test Muscle",
                Description = "Test Description"
            };
            _context.Muscles.Add(muscle);
            await _context.SaveChangesAsync();
            //act
            var musclesCount = await _context.Muscles.CountAsync();
            var result = await muscleRepository.CreateMuscle(muscle);
            var newMuscelsCount = await _context.Muscles.CountAsync();
            //assert
            result.Should().BeNull();
            newMuscelsCount.Should().Be(musclesCount);
        }

        [Fact]
        public async Task DeleteMuscle_ShouldReturnTrue_WhenDeleted()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC");
            //act
            var musclesCount = await _context.Muscles.CountAsync();
            var result = await muscleRepository.DeleteMuscle(muscleId);
            var newMuscelsCount = await _context.Muscles.CountAsync();
            //assert
            result.Should().BeTrue();
            newMuscelsCount.Should().Be(musclesCount - 1);
        }

        [Fact]
        public async Task DeleteMuscle_ShouldReturnFalse_WhenMuscleNotFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DF");
            //act
            var musclesCount = await _context.Muscles.CountAsync();
            var result = await muscleRepository.DeleteMuscle(muscleId);
            var newMuscelsCount = await _context.Muscles.CountAsync();
            //assert
            result.Should().BeFalse();
            newMuscelsCount.Should().Be(musclesCount);
        }

        [Fact]
        public async Task GetMuscleById_ShouldReturnMuscle_WhenFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835");
            //act
            var result = await muscleRepository.GetMuscleById(muscleId);
            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(muscleId);
        }
        [Fact]
        public async Task GetMuscleById_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0836");
            //act
            var result = await muscleRepository.GetMuscleById(muscleId);
            //assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task GetMuscles_ShouldReturnListOfMuscles_WhenFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            //act
            var result = await muscleRepository.GetMuscles();
            //assert
            result.Should().NotBeNull();
            result.Count.Should().Be(16);
        }
        [Fact]
        public async Task GetMuscles_ShouldReturnEmptyList_WhenNotFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            _context.Muscles.RemoveRange(_context.Muscles);
            await _context.SaveChangesAsync();
            //act
            var result = await muscleRepository.GetMuscles();
            //assert
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }
        [Fact]
        public async Task UpdateMuscle_ShouldReturnNewMuscle_WhenUpdated()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835");
            var muscle = await _context.Muscles.FindAsync(muscleId);
            var newMuscle = new UpdateMuscleDto
            {
                Name = "New Name",
                Description = "New Description"
            };
            //act
            await muscleRepository.UpdateMuscle(muscle!, newMuscle);
            var result = await muscleRepository.GetMuscleById(muscle!.Id);
            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(muscleId);
            result.Name.Should().Be(newMuscle.Name);
            result.Description.Should().Be(newMuscle.Description);
        }
        [Fact]
        public async Task UpdateMuscle_ShouldReturnNull_WhenMuscleNotFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0836");
            var muscle = await _context.Muscles.FindAsync(muscleId);
            var newMuscle = new UpdateMuscleDto
            {
                Name = "New Name",
                Description = "New Description"
            };
            //act
            var result = await muscleRepository.UpdateMuscle(muscle!, newMuscle);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetMuscleByName_ShouldReturnMuscle_WhenFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleName = "Biceps";
            //act
            var result = await muscleRepository.GetMuscleByName(muscleName);
            //assert
            result.Should().NotBeNull();
            result!.Name.Should().Be(muscleName);
        }

        [Fact]
        public async Task GetMuscleByName_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            var muscleName = "Test";
            //act
            var result = await muscleRepository.GetMuscleByName(muscleName);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetMuscleByName_ShouldReturnNull_WhenNameIsNull()
        {
            //arrange
            var muscleRepository = new SQLMuscleRepository(_context);
            string? muscleName = null;
            //act
            var result = await muscleRepository.GetMuscleByName(muscleName);
            //assert
            result.Should().BeNull();
        }
    }
}
