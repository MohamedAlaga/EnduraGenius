
using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using EnduraGenius.API.Repositories.UserRepository;
using AutoMapper;
using FakeItEasy;



namespace EnduraGenius.API.Tests.Repositories.UserRepositoryTests
{
    public class SQLUserRepositoryTests
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        private readonly IMapper _mapper;
        public SQLUserRepositoryTests()
        {
            _dbcontext = new EnduraGeniusTestingDBContexts().GetDBContextWithData().Result;
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenFound()
        {
            //arrange
            var userRepository = new SQLUserRepository(_dbcontext, _mapper);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await userRepository.GetUserById(userId);
            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var userRepository = new SQLUserRepository(_dbcontext, _mapper);
            var userId = Guid.NewGuid().ToString();
            //act
            var result = await userRepository.GetUserById(userId);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task EditUserBodyData_ShouldReturnUser_WhenEdited()
        {
            //arrange
            var userRepository = new SQLUserRepository(_dbcontext, _mapper);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            float weight = 80;
            int tall = 180;
            int age = 25;
            bool isMale = true;
            bool isPublic = true;
            //act
            await userRepository.EditUserBodyData(userId, weight, tall, age, isMale, isPublic);
            var result = await userRepository.GetUserById(userId);
            //assert
            result.Should().NotBeNull();
            result.WeightInKg.Should().Be(weight);
            result.TallInCm.Should().Be(tall);
            result.Age.Should().Be(age);
            result.IsMale.Should().Be(isMale);
            result.isPublic.Should().Be(isPublic);
        }
        [Fact]
        public async Task EditUserBodyData_ShouldReturnNull_WhenNotEdited()
        {
            //arrange
            var userRepository = new SQLUserRepository(_dbcontext, _mapper);
            var userId = Guid.NewGuid().ToString();
            float weight = 80;
            int tall = 180;
            int age = 25;
            bool isMale = true;
            bool isPublic = true;
            //act
            var result = await userRepository.EditUserBodyData(userId, weight, tall, age, isMale, isPublic);
            //assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task EditUserPoints_ShouldReturnUser_WhenEdited()
        {
            //arrange
            var userRepository = new SQLUserRepository(_dbcontext, _mapper);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            int points = 100;
            //act
            await userRepository.EditUserPoints(userId, points);
            var result = await userRepository.GetUserById(userId);
            //assert
            result.Should().NotBeNull();
            result.Points.Should().Be(points);
        }
        [Fact]
        public async Task EditUserPoints_ShouldReturnNull_WhenNotEdited()
        {
            //arrange
            var userRepository = new SQLUserRepository(_dbcontext, _mapper);
            var userId = Guid.NewGuid().ToString();
            int points = 100;
            //act
            var result = await userRepository.EditUserPoints(userId, points);
            //assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task AddUserPoints_ShouldReturnUser_WhenAdded()
        {
            //arrange
            var userRepository = new SQLUserRepository(_dbcontext, _mapper);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            int points = 100;
            var user = await userRepository.GetUserById(userId);
            int oldPoints = user!.Points;
            //act
            await userRepository.AddUserPoints(userId, points);
            var result = await userRepository.GetUserById(userId);
            //assert
            result.Should().NotBeNull();
            result.Points.Should().Be(oldPoints + points);
        }
    }
}
