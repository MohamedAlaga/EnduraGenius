using AutoMapper;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.InbodyRepository;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace EnduraGenius.API.Tests.Controller
{
    public class InbodyControllerTests
    {
        private readonly IInbodyRepository _inbodyRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public InbodyControllerTests()
        {
            this._inbodyRepository = A.Fake<IInbodyRepository>();
            this._mapper = A.Fake<IMapper>();
            this._authRepository = A.Fake<IAuthRepository>();
        }

        [Fact]
        // Test for GetInbody method in InbodyController
        public async Task InbodyController_GetInbody_ReturnsInbody()
        {
            // Arrange
            var inbody = new List<Inbody>();
            var userId = "1";
            A.CallTo(() => _inbodyRepository.GetInbodyByUserId(A<string>._)).Returns(inbody);
            var inbodyDTO = new List<InbodyResponseDTO>();
            A.CallTo(() => _mapper.Map<List<InbodyResponseDTO>>(inbody)).Returns(inbodyDTO);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.GetInbody();
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(inbodyDTO);
        }
        [Fact]
        // Test for GetInbody method in InbodyController but the user is not authorized
        public async Task InbodyController_GetInbody_ReturnUnAuth()
        {
            // Arrange
            var inbody = new List<Inbody>();
            string? userId = null;
            A.CallTo(() => _inbodyRepository.GetInbodyByUserId(A<string>._)).Returns(inbody);
            var inbodyDTO = new List<InbodyResponseDTO>();
            A.CallTo(() => _mapper.Map<List<InbodyResponseDTO>>(inbody)).Returns(inbodyDTO);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.GetInbody();
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        // Test for PostInbody method in InbodyController
        public async Task InbodyController_PostInbody_ReturnsInbody()
        {
            // Arrange
            var requestInbodyDTO = new RequestInbodyDTO();
            var inbody = new Inbody();
            var userId = "1";
            A.CallTo(() => _inbodyRepository.InsertInbodyAsync(A<string>._, A<int>._, A<string>._)).Returns(inbody);
            var inbodyDTO = new InbodyResponseDTO();
            A.CallTo(() => _mapper.Map<InbodyResponseDTO>(inbody)).Returns(inbodyDTO);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.PostInbody(requestInbodyDTO);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
            result.As<CreatedAtActionResult>().Value.Should().Be(inbodyDTO);
        }

        [Fact]
        // Test for PostInbody method in InbodyController but the user is not authorized
        public async Task InbodyController_PostInbody_ReturnUnAuth()
        {
            // Arrange
            var requestInbodyDTO = new RequestInbodyDTO();
            var inbody = new Inbody();
            string? userId = null;
            A.CallTo(() => _inbodyRepository.InsertInbodyAsync(A<string>._, A<int>._, A<string>._)).Returns(inbody);
            var inbodyDTO = new InbodyResponseDTO();
            A.CallTo(() => _mapper.Map<InbodyResponseDTO>(inbody)).Returns(inbodyDTO);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.PostInbody(requestInbodyDTO);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        // Test for GetInbodyById method in InbodyController
        public async Task InbodyController_GetInbodyById_ReturnsInbody(string id)
        {
            // Arrange
            var inbody = new Inbody();
            var userId = "1";
            A.CallTo(() => _inbodyRepository.GetInbodyAsync(A<Guid>._, A<string>._)).Returns(inbody);
            var inbodyDTO = new InbodyResponseDTO();
            A.CallTo(() => _mapper.Map<InbodyResponseDTO>(inbody)).Returns(inbodyDTO);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.GetInbodyById(Guid.Parse(id));
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(inbodyDTO);
        }

        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        // Test for GetInbodyById method in InbodyController but the user is not authorized
        public async Task InbodyController_GetInbodyById_ReturnUnAuth(string id)
        {
            // Arrange
            var inbody = new Inbody();
            string? userId = null;
            A.CallTo(() => _inbodyRepository.GetInbodyAsync(A<Guid>._, A<string>._)).Returns(inbody);
            var inbodyDTO = new InbodyResponseDTO();
            A.CallTo(() => _mapper.Map<InbodyResponseDTO>(inbody)).Returns(inbodyDTO);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.GetInbodyById(Guid.Parse(id));
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        // Test for GetInbodyById method in InbodyController but did not found the inbody
        public async Task InbodyController_GetInbodyById_ReturnNotFound(string id)
        {
            // Arrange
            Inbody? inbody = null;
            string? userId = "1";
            A.CallTo(() => _inbodyRepository.GetInbodyAsync(A<Guid>._, A<string>._)).Returns(inbody);
            var inbodyDTO = new InbodyResponseDTO();
            A.CallTo(() => _mapper.Map<InbodyResponseDTO>(inbody)).Returns(inbodyDTO);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.GetInbodyById(Guid.Parse(id));
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        // Test for DeleteInbody method in InbodyController
        public async Task InbodyController_DeleteInbody_ReturnsOk(string id)
        {
            // Arrange
            var userId = "1";
            A.CallTo(() => _inbodyRepository.DeleteInbody(A<Guid>._, A<string>._)).Returns(true);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.DeleteInbody(Guid.Parse(id));
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        // Test for DeleteInbody method in InbodyController but the user is not authorized
        public async Task InbodyController_DeleteInbody_ReturnUnAuth(string id)
        {
            // Arrange
            string? userId = null;
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.DeleteInbody(Guid.Parse(id));
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Theory]
        [InlineData("2ea38af5-010c-4718-b1ed-48131c4964d0")]
        // Test for DeleteInbody method in InbodyController but did not found the inbody
        public async Task InbodyController_DeleteInbody_ReturnNotFound(string id)
        {
            // Arrange
            var userId = "1";
            A.CallTo(() => _inbodyRepository.DeleteInbody(A<Guid>._, A<string>._)).Returns(false);
            A.CallTo(() => _authRepository.GetCurrentUserId()).Returns(userId);
            var controller = new InbodyController(_mapper, _inbodyRepository, _authRepository);
            // Act
            var result = await controller.DeleteInbody(Guid.Parse(id));
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
