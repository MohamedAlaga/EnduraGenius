
using EnduraGenius.API.Data;
using EnduraGenius.API.Tests.DBcontexts;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using EnduraGenius.API.Repositories.PlanRepositories;

namespace EnduraGenius.API.Tests.Repositories.PlanRepositoriesTests
{
    public class SQLPLansRepositoryTests
    {
        private readonly EnduraGeniusDBContext _context;

        public SQLPLansRepositoryTests()
        {
            _context = new EnduraGeniusTestingDBContexts().GetDBContextWithData().Result;
        }

        [Fact]
        public async Task CreatePlan_ShouldReturnPlan_WhenCreated()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            string planName = "Test Plan";
            string planDescription = "Test Description";
            string userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            //act
            var plansCount = await _context.Plans.CountAsync();
            var result = await planRepository.CreatePlan(planName,planDescription,userId,false);
            var newPlansCount = await _context.Plans.CountAsync();
            //assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Name.Should().Be(planName);
            result.Descreption.Should().Be(planDescription);
            newPlansCount.Should().Be(plansCount + 1);
        }

        [Fact]
        public async Task DeletePlan_ShouldReturnTrue_WhenDeleted()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            var planId = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0D");
            //act
            var plansCount = await _context.Plans.CountAsync();
            var result = await planRepository.DeletePlan(planId , "a4059c44-8a45-4200-bfa8-bd618696d3ea");
            var newPlansCount = await _context.Plans.CountAsync();
            //assert
            result.Should().BeTrue();
            newPlansCount.Should().Be(plansCount - 1);
        }

        [Fact]
        public async Task DeletePlan_ShouldReturnFalse_WhenNotDeleted()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            var planId = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0C");
            //act
            var plansCount = await _context.Plans.CountAsync();
            var result = await planRepository.DeletePlan(planId, "a4059c44-8a45-4200-bfa8-bd618696d3e");
            var newPlansCount = await _context.Plans.CountAsync();
            //assert
            result.Should().BeFalse();
            newPlansCount.Should().Be(plansCount);
        }

        [Fact]
        public async Task GetPlanById_ShouldReturnPlan_WhenFound()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            var planId = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0D");
            //act
            var result = await planRepository.GetPlanById(planId, "a4059c44-8a45-4200-bfa8-bd618696d3ea");
            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(planId);
        }

        [Fact]
        public async Task GetPlanById_ShouldReturnNull_WhenNotFound()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            var planId = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0C");
            //act
            var result = await planRepository.GetPlanById(planId, "a4059c44-8a45-4200-bfa8-bd618696d3ea");
            //assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task GetPlansByCreatorId_ShouldReturnPlans_WhenFound()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            //act
            var result = await planRepository.GetPlansByCreatorId("a4059c44-8a45-4200-bfa8-bd618696d3ea");
            //assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetPlansByCreatorId_ShouldReturnEmptyList_WhenNotFound()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            //act
            var result = await planRepository.GetPlansByCreatorId("a4059c44-8a45-4200-bfa8-bd618696d3eb");
            //assert
            result.Should().BeEmpty();
        }
        [Fact]
        public async Task GetPublicPlans_ShouldReturnPlans_WhenFound()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            //act
            var result = await planRepository.GetPublicPlans("a4059c44-8a45-4200-bfa8-bd618696d3ea");
            var userPrivatePlans = await planRepository.GetPlansByCreatorId("a4059c44-8a45-4200-bfa8-bd618696d3eb");
            //assert
            result.Should().NotBeEmpty();
            foreach (var plan in userPrivatePlans)
            {
                result.Should().Contain(plan);
            }
            result.Should().HaveCountGreaterThanOrEqualTo(userPrivatePlans.Count());
        }
        [Fact]
        public async Task UpdatePlan_ShouldReturnNewPlan_WhenUpdated()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            var planId = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0D");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            string newPlanName = "New Plan Name";
            string newPlanDescription = "New Plan Description";
            //act
            var result = await planRepository.UpdatePlan(planId, userId,newPlanName, newPlanDescription,true,new List<CreatePlanWorkoutsDto> { });
            //assert
            result.Should().NotBeNull();
            result.Name.Should().Be(newPlanName);
            result.Descreption.Should().Be(newPlanDescription);
            result.IsPublic.Should().BeTrue();
            result.PlanCreatedBy.Should().Be(userId);
            result.Id.Should().Be(planId);
        }
        [Fact]
        public async Task UpdatePlan_ShouldReturnNull_WhenNotUpdated()
        {
            //arrange
            var planRepository = new SQLPLansRepository(_context);
            var planId = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0C");
            var userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            string newPlanName = "New Plan Name";
            string newPlanDescription = "New Plan Description";
            //act
            var result = await planRepository.UpdatePlan(planId, userId, newPlanName, newPlanDescription, true, new List<CreatePlanWorkoutsDto> { });
            //assert
            result.Should().BeNull();
        }
    }
}
