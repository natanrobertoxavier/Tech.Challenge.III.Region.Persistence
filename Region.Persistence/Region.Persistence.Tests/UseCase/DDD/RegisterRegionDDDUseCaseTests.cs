using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using Region.Persistence.Application.Services.LoggedUser;
using Region.Persistence.Application.UseCase.DDD;
using Region.Persistence.Communication.Request;
using Region.Persistence.Communication.Request.Enum;
using Region.Persistence.Domain.Entities;
using Region.Persistence.Domain.Messages.DomaiEvents;
using Region.Persistence.Domain.ResultServices;
using Region.Persistence.Domain.Services;
using Region.Persistence.Exceptions;
using Region.Persistence.Exceptions.ExceptionBase;
using Serilog;
using TokenService.Manager.Controller;
using Xunit;

namespace Region.Persistence.Tests.UseCase.DDD;
public class RegisterRegionDDDUseCaseTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILoggedUser> _loggedUserMock;
    private readonly Mock<TokenController> _tokenControllerMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IRegionQueryServiceApi> _regionQueryServiceApiMock;
    private readonly Mock<ILogger> _loggerMock;

    private readonly RegisterRegionDDDUseCase _sut;

    public RegisterRegionDDDUseCaseTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggedUserMock = new Mock<ILoggedUser>();
        _tokenControllerMock = new Mock<TokenController>(10, "YTJVPzlCM3Q1KVkoPGM1PlgqNit8MjR4O3Jba1ZR");
        _mediatorMock = new Mock<IMediator>();
        _regionQueryServiceApiMock = new Mock<IRegionQueryServiceApi>();
        _loggerMock = new Mock<ILogger>();

        _sut = new RegisterRegionDDDUseCase(
            _regionQueryServiceApiMock.Object,
            _mediatorMock.Object,
            _mapperMock.Object,
            _loggedUserMock.Object,
            _loggerMock.Object,
            _tokenControllerMock.Object
        );
    }

    [Fact]
    public async Task RegisterDDDAsync_ShouldReturnSuccess_WhenAllValidationsPass()
    {
        // Arrange
        var request = new RequestRegionDDDJson { DDD = 11, Region = RegionRequestEnum.Sudeste };

        var loggedUser = new User(Guid.NewGuid(), DateTime.UtcNow, "John Cena", "test@example.com", "any-password");

        var responseRegionQueryApi = new Result<ThereIsDDDNumberResult>();

        var responseApi = responseRegionQueryApi.Success(new ThereIsDDDNumberResult() { ThereIsDDDNumber = false });

        _loggedUserMock.Setup(x => x.RecoverUser()).ReturnsAsync(loggedUser);

        _regionQueryServiceApiMock
            .Setup(x => x.ThereIsDDDNumber(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(responseApi);

        _mapperMock
            .Setup(x => x.Map<Domain.Entities.RegionDDD>(request))
            .Returns(new Domain.Entities.RegionDDD { DDD = request.DDD });

        // Act
        var result = await _sut.RegisterDDDAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Message.Should().Be("Cadastro em processamento.");
        _mediatorMock.Verify(x => x.Publish(It.IsAny<RegionCreateDomainEvent>(), default), Times.Once);
    }

    [Fact]
    public async Task RegisterDDDAsync_ShouldReturnFailure_WhenValidationFails()
    {
        // Arrange
        var request = new RequestRegionDDDJson { DDD = 123, Region = RegionRequestEnum.Sudeste };

        var loggedUser = new User(Guid.NewGuid(), DateTime.UtcNow, "John Cena", "test@example.com", "any-password");

        var responseRegionQueryApi = new Result<ThereIsDDDNumberResult>();

        var responseApi = responseRegionQueryApi.Success(new ThereIsDDDNumberResult() { ThereIsDDDNumber = false });

        _loggedUserMock.Setup(x => x.RecoverUser()).ReturnsAsync(loggedUser);

        _regionQueryServiceApiMock
            .Setup(x => x.ThereIsDDDNumber(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(responseApi);

        // Act
        var result = await _sut.RegisterDDDAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Contains("DDD deve ser entre 10 e 99").Should().BeTrue();
        _mediatorMock.Verify(x => x.Publish(It.IsAny<RegionCreateDomainEvent>(), default), Times.Never);
    }

    [Fact]
    public async Task RegisterDDDAsync_ShouldReturnFailure_WhenApiResponseFails()
    {
        // Arrange
        var request = new RequestRegionDDDJson { DDD = 123, Region = RegionRequestEnum.Sudeste };

        var loggedUser = new User(Guid.NewGuid(), DateTime.UtcNow, "John Cena", "test@example.com", "any-password");

        var responseRegionQueryApi = new Result<ThereIsDDDNumberResult>();

        var responseApi = responseRegionQueryApi.Failure("API error");

        _loggedUserMock.Setup(x => x.RecoverUser()).ReturnsAsync(loggedUser);

        _regionQueryServiceApiMock
            .Setup(x => x.ThereIsDDDNumber(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(responseApi);

        // Act
        var result = await _sut.RegisterDDDAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Contains("API error").Should().BeTrue();
        _mediatorMock.Verify(x => x.Publish(It.IsAny<RegionCreateDomainEvent>(), default), Times.Never);
    }

    [Fact]
    public async Task RegisterDDDAsync_ShouldLogError_WhenUnhandledExceptionOccurs()
    {
        // Arrange
        var request = new RequestRegionDDDJson { DDD = 123, Region = RegionRequestEnum.Sudeste };

        _loggedUserMock.Setup(x => x.RecoverUser()).ThrowsAsync(new Exception("Unexpected error"));

        // Act
        var result = await _sut.RegisterDDDAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("There are an error: Unexpected error");
        _loggerMock.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
    }
}
