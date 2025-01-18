
using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using EnduraGenius.API.Repositories.InbodyRepository;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace EnduraGenius.API.Tests.Repositories.InbodyRepositoryTests
{
    public class SQLInbodyRepositoryTests
    {
        private readonly EnduraGeniusDBContext _context;
        public SQLInbodyRepositoryTests()
        {
            var db = new EnduraGeniusTestingDBContexts();
            _context = db.GetDBContextWithData().Result;
        }

        [Fact]
        public async Task GetInbodyByUserId_ShouldReturnListOfInbodies_WhenFoundUser()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            //act
            var result = await inbodyRepository.GetInbodyByUserId(userId);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetInbodyByUserId_ShouldReturnEmptyList_WhenUserNotFound()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3eb";
            //act
            var result = await inbodyRepository.GetInbodyByUserId(userId);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetInbodyAsync_ShouldReturnInbody_WhenFound()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var inbodyId = Guid.Parse("6F98D438-438E-4675-3498-08DD3036A4F8");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            //act
            var result = await inbodyRepository.GetInbodyAsync(inbodyId, userId);
            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(inbodyId);
        }

        [Fact]
        public async Task GetInbodyAsync_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var inbodyId = Guid.Parse("6F98D438-438E-4675-3498-08DD3036A4F9");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            //act
            var result = await inbodyRepository.GetInbodyAsync(inbodyId, userId);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task InsertInbodyAsync_ShouldReturnInbody_WhenInserted()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            var activityLevel = 1;
            var name = "Test";
            //act
            var inbodyCount = await _context.Inbodies.CountAsync();
            var result = await inbodyRepository.InsertInbodyAsync(userId, activityLevel, name);
            var newInbodyCount = await _context.Inbodies.CountAsync();
            //assert
            result.Should().NotBeNull();
            result.userId.Should().Be(userId);
            result.Name.Should().Be(name);
            newInbodyCount.Should().Be(inbodyCount + 1);
        }

        [Fact]
        public async Task InsertInbodyAsync_ShouldReturnNull_WhenUserNotFound()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3eb";
            var activityLevel = 1;
            var name = "Test";
            //act
            var result = await inbodyRepository.InsertInbodyAsync(userId, activityLevel, name);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteInbody_ShouldReturnTrue_WhenDeleted()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var inbodyId = Guid.Parse("6613C126-BD75-4E04-5C53-08DD30376AD2");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            //act
            var inbodyCount = await _context.Inbodies.CountAsync();
            var result = await inbodyRepository.DeleteInbody(inbodyId, userId);
            var newInbodyCount = await _context.Inbodies.CountAsync();
            //assert
            result.Should().BeTrue();
            newInbodyCount.Should().Be(inbodyCount - 1);
        }

        [Fact]
        public async Task DeleteInbody_ShouldReturnFalse_WhenInbodyNotFound()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var inbodyId = Guid.Parse("6613C126-BD75-4E04-5C53-08DD30376AD3");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            //act
            var inbodyCount = await _context.Inbodies.CountAsync();
            var result = await inbodyRepository.DeleteInbody(inbodyId, userId);
            var newInbodyCount = await _context.Inbodies.CountAsync();
            //assert
            result.Should().BeFalse();
            newInbodyCount.Should().Be(inbodyCount);
        }

        [Fact]
        public async Task DeleteInbody_ShouldReturnFalse_WhenUserNotFound()
        {
            //arrange
            var inbodyRepository = new SQLInbodyRepository(_context);
            var inbodyId = Guid.Parse("6613C126-BD75-4E04-5C53-08DD30376AD2");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3eb";
            //act
            var inbodyCount = await _context.Inbodies.CountAsync();
            var result = await inbodyRepository.DeleteInbody(inbodyId, userId);
            var newInbodyCount = await _context.Inbodies.CountAsync();
            //assert
            result.Should().BeFalse();
            newInbodyCount.Should().Be(inbodyCount);
        }
    }
}
