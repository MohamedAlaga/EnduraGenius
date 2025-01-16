using Xunit;
using FakeItEasy;
using FluentAssertions;
using EnduraGenius.API.Controllers;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Repositories.MuscleRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnduraGenius.Tests.Controllers
{
    public class MusclesControllerTests
    {
        private readonly IMuscleRepository _fakeMuscleRepository;
        private readonly IMapper _fakeMapper;
        private readonly MusclesController _controller;

        public MusclesControllerTests()
        {
            _fakeMuscleRepository = A.Fake<IMuscleRepository>();
            _fakeMapper = A.Fake<IMapper>();
            _controller = new MusclesController(_fakeMuscleRepository, _fakeMapper);
        }

        [Fact]
        public async Task GetMuscles_ShouldReturnOkWithListOfMuscles()
        {
            // Arrange
            var muscles = new List<Muscle> { new Muscle { Id = Guid.NewGuid(), Name = "Bicep" } };
            A.CallTo(() => _fakeMuscleRepository.GetMuscles()).Returns(Task.FromResult((List<Muscle>)muscles));

            // Act
            var result = await _controller.GetMuscles();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(muscles);
        }


        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        public async Task GetMuscleById_ShouldReturnOkWithMuscle_WhenMuscleExists(Guid id)
        {
            // Arrange
            var muscle = new Muscle { Id = id, Name = "Tricep" };
            A.CallTo(() => _fakeMuscleRepository.GetMuscleById(id)).Returns(Task.FromResult(muscle));

            // Act
            var result = await _controller.GetMuscleById(id);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(muscle);
        }

        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        public async Task GetMuscleById_ShouldReturnNotFound_WhenMuscleDoesNotExist(Guid id)
        {
            // Arrange
            A.CallTo(() => _fakeMuscleRepository.GetMuscleById(id)).Returns(Task.FromResult<Muscle?>(null));

            // Act
            var result = await _controller.GetMuscleById(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateMuscle_ShouldReturnCreatedAtAction_WhenMuscleIsCreated()
        {
            // Arrange
            var createDto = new CreateMuscleDTO { Name = "Bicep" };
            var muscle = new Muscle { Id = Guid.NewGuid(), Name = createDto.Name };
            A.CallTo(() => _fakeMapper.Map<Muscle>(createDto)).Returns(muscle);
            A.CallTo(() => _fakeMuscleRepository.CreateMuscle(muscle)).Returns(Task.FromResult(muscle));

            // Act
            var result = await _controller.CreateMuscle(createDto);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.Value.Should().BeEquivalentTo(createDto);
        }

        [Fact]
        public async Task UpdateMuscle_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var oldMuscle = new Muscle { Id = id, Name = "Bicep" };
            var updateDto = new UpdateMuscleDto { Name = "Tricep" };
            var updatedMuscle = new Muscle { Id = id, Name = updateDto.Name };

            A.CallTo(() => _fakeMuscleRepository.GetMuscleById(id)).Returns(Task.FromResult(oldMuscle));
            A.CallTo(() => _fakeMuscleRepository.UpdateMuscle(oldMuscle, updateDto)).Returns(Task.FromResult(updatedMuscle));

            // Act
            var result = await _controller.UpdateMuscle(id, updateDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(updatedMuscle);
        }

        [Fact]
        public async Task UpdateMuscle_ShouldReturnNotFound_WhenMuscleDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateMuscleDto { Name = "Tricep" };
            A.CallTo(() => _fakeMuscleRepository.GetMuscleById(id)).Returns(Task.FromResult<Muscle>(null));

            // Act
            var result = await _controller.UpdateMuscle(id, updateDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteMuscle_ShouldReturnOk_WhenDeleteIsSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            A.CallTo(() => _fakeMuscleRepository.DeleteMuscle(id)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeleteMuscle(id);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteMuscle_ShouldReturnNotFound_WhenMuscleDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            A.CallTo(() => _fakeMuscleRepository.DeleteMuscle(id)).Returns(Task.FromResult(false));

            // Act
            var result = await _controller.DeleteMuscle(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}