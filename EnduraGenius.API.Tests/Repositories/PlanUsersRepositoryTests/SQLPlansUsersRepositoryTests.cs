
using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using EnduraGenius.API.Repositories.PlanRepositories;

namespace EnduraGenius.API.Tests.Repositories.PlanUsersRepositoryTests
{
    public class SQLPlansUsersRepositoryTests
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        private readonly SQLPLansRepository _planRepository;
        public SQLPlansUsersRepositoryTests()
        {
            _dbcontext = new EnduraGeniusTestingDBContexts().GetDBContextWithData().Result;
            _planRepository = new SQLPLansRepository(_dbcontext);
        }

        [Fact]
        public async Task CreatePlanUser_ShouldReturnPlanUser_WhenCreated()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var plan = await _dbcontext.Plans.FirstOrDefaultAsync();
            var user = await _dbcontext.Users.FirstOrDefaultAsync();
            //act
            var planUsersCount = await _dbcontext.PlansUsers.CountAsync();
            var result = await planUserRepository.CreatePlanUser(plan!, user!.Id);
            var newPlanUsersCount = await _dbcontext.PlansUsers.CountAsync();
            //assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.PlanId.Should().Be(plan!.Id);
            result.UserId.Should().Be(user.Id);
            newPlanUsersCount.Should().Be(planUsersCount + 1);
        }
        [Fact]
        public async Task CreatePlanUser_ShouldReturnNull_WhenNotCreated()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var plan = await _dbcontext.Plans.FirstOrDefaultAsync();
            var user = Guid.NewGuid().ToString();
            var planUser = await _dbcontext.PlansUsers.FirstOrDefaultAsync();
            //act
            var planUsersCount = await _dbcontext.PlansUsers.CountAsync();
            var result = await planUserRepository.CreatePlanUser(plan!, user);
            var newPlanUsersCount = await _dbcontext.PlansUsers.CountAsync();
            //assert
            result.Should().BeNull();
            newPlanUsersCount.Should().Be(planUsersCount);
        }
        [Fact]
        public async Task DeletePlanUser_ShouldReturnTrue_WhenDeleted()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planUserId = Guid.Parse("70A73454-B1A8-48CC-E09B-08DD2FB0EF5D");
            //act
            var planUsersCount = await _dbcontext.PlansUsers.CountAsync();
            var result = await planUserRepository.DeletePlanUser(planUserId);
            var newPlanUsersCount = await _dbcontext.PlansUsers.CountAsync();
            //assert
            result.Should().BeTrue();
            newPlanUsersCount.Should().Be(planUsersCount - 1);
        }

        [Fact]
        public async Task DeletePlanUser_ShouldReturnFalse_WhenNotDeleted()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planUserId = Guid.NewGuid();
            //act
            var planUsersCount = await _dbcontext.PlansUsers.CountAsync();
            var result = await planUserRepository.DeletePlanUser(planUserId);
            var newPlanUsersCount = await _dbcontext.PlansUsers.CountAsync();
            //assert
            result.Should().BeFalse();
            newPlanUsersCount.Should().Be(planUsersCount);
        }
        [Fact]
        public async Task GetPlanUserById_ShouldReturnPlanUser_WhenFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planUserId = Guid.Parse("70A73454-B1A8-48CC-E09B-08DD2FB0EF5D");
            //act
            var result = await planUserRepository.GetPlanUserById(planUserId);
            //assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(planUserId);
        }
        [Fact]
        public async Task GetPlanUserById_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planUserId = Guid.NewGuid();
            //act
            var result = await planUserRepository.GetPlanUserById(planUserId);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetPlansUserByUserId_ShouldReturnPlanUser_WhenFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planUserRepository.GetPlansUserByUserId(userId);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
        [Fact]
        public async Task GetPlansUserByUserId_ShouldReturnEmptyList_WhenNotFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var userId = Guid.NewGuid().ToString();
            //act
            var result = await planUserRepository.GetPlansUserByUserId(userId);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetUserCurrentPlan_ShouldReturnPlanUser_WhenFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planUserRepository.GetUserCurrentPlan(userId);
            //assert
            result.Should().NotBeNull();
            result!.UserId.Should().Be(userId);
        }

        [Fact]
        public async Task GetUserCurrentPlan_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var userId = Guid.NewGuid().ToString();
            //act
            var result = await planUserRepository.GetUserCurrentPlan(userId);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SetCurrentPlan_ShouldReturnPlansUsers_WhenFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planUserRepository.SetCurrentPlan(planId, userId);
            var currentUserPlan = await planUserRepository.GetUserCurrentPlan(userId);
            //assert
            result.Should().NotBeNull();
            result!.PlanId.Should().Be(planId);
            result.UserId.Should().Be(userId);
            currentUserPlan!.PlanId.Should().Be(planId);
        }

        [Fact]
        public async Task SetCurrentPlan_ShouldReturnNull_WhenUserNotFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C");
            var userId = Guid.NewGuid().ToString();
            //act
            var result = await planUserRepository.SetCurrentPlan(planId, userId);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SetCurrentPlan_ShouldReturnNull_WhenPlanNotFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planId = Guid.NewGuid();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planUserRepository.SetCurrentPlan(planId, userId);
            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UnsubscibeUserFromPlan_ShouldReturnTrue_WhenUnsubscribed()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C");
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planUserRepository.UnsubscibeUserFromPlan(userId, planId);
            var currentUserPlan = await planUserRepository.GetUserCurrentPlan(userId);
            //assert
            result.Should().BeTrue();
            currentUserPlan!.Id.Should().NotBe(planId);
        }

        [Fact]
        public async Task UnsubscibeUserFromPlan_ShouldReturnFalse_WhenPlanNotFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planId = Guid.NewGuid();
            var userId = "29b0975c-b32f-4842-988a-e038f0470fde";
            //act
            var result = await planUserRepository.UnsubscibeUserFromPlan(userId, planId);
            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UnsubscibeUserFromPlan_ShouldReturnFalse_WhenUserNotFound()
        {
            //arrange
            var planUserRepository = new SQLPlansUsersRepository(_dbcontext, _planRepository);
            var planId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C");
            var userId = Guid.NewGuid().ToString();
            //act
            var result = await planUserRepository.UnsubscibeUserFromPlan(userId, planId);
            //assert
            result.Should().BeFalse();
        }
    }
}
