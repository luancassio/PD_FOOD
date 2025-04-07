using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using PD_FOOD.Application.Commands.UserNotifications;
using PD_FOOD.Application.Queries.UserNotifications;
using PD_FOOD.Controllers;
using PD_FOOD.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Interfaces;


public class UserNotificationControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<UserNotificationsController>> _loggerMock;
    private readonly UserNotificationsController _controller;

    public UserNotificationControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<UserNotificationsController>>();
        _controller = new UserNotificationsController(_mediatorMock.Object, _loggerMock.Object);
    }
    [Fact]
    public async Task GetAll_ShouldReturnOkWithData()
    {
        // Arrange
        var expectedList = new List<UserNotification>
        {
            new() { Email = "teste@email.com", Name = "Luan", Hour = new(18, 0), IsActive = true },
            new() { Email = "exemplo@email.com", Name = "Maria", Hour = new(12, 0), IsActive = false }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllUserNotificationsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedList);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var data = okResult.Value!.GetType().GetProperty("data")!.GetValue(okResult.Value);
        data.Should().BeEquivalentTo(expectedList);
    }
    [Fact]
    public async Task UpdateIsActiveNotification_ShouldReturnOkWithResult()
    {
        // Arrange
        var command = new UpdateUserNotificationIsActiveCommand
        {
            Id = new Guid("3a8e1f4b-7c2d-45f0-9b12-6d34e890a1c2")
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateIsActiveNotification(command);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var data = okResult.Value!.GetType().GetProperty("data")!.GetValue(okResult.Value);
        data.Should().Be(true);
    }
    public async Task UpdateIsActiveNotification_ShouldReturnBadRequest_WhenUserNotExists()
    {
        // Arrange
        var command = new UpdateUserNotificationIsActiveCommand
        {
            Id = Guid.NewGuid()
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<UpdateUserNotificationIsActiveCommand>(c => c.Id == command.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateIsActiveNotification(command);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();

        var badRequest = result as BadRequestObjectResult;
        badRequest!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        var problemDetails = badRequest.Value as ValidationProblemDetails;
        problemDetails.Should().NotBeNull();
        problemDetails!.Errors["Message"].Should().Contain("Id is not find");
    }
    [Fact]
    public async Task Should_NotSendEmail_When_UsersExist_ButNoneIsActive()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDb_NoActiveUsers"));

        var emailMock = new Mock<IEmailSender>();
        services.AddSingleton(emailMock.Object);

        var provider = services.BuildServiceProvider();

        using (var scope = provider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Inserir usuários, mas todos com IsActive = false
            db.UserNotifications.AddRange(new List<UserNotification>
        {
            new UserNotification
            {
                Id = Guid.NewGuid(),
                Name = "User 1",
                Email = "user1@email.com",
                IsActive = false,
                Hour = new TimeOnly(18, 0)
            },
            new UserNotification
            {
                Id = Guid.NewGuid(),
                Name = "User 2",
                Email = "user2@email.com",
                IsActive = false,
                Hour = new TimeOnly(18, 0)
            }
        });

            db.FinancialTransactions.AddRange(new List<FinancialTransaction>
        {
            new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.Income,
                Date = DateTime.Today,
                Description = "Receita",
                Amount = 100
            },
            new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.Expense,
                Date = DateTime.Today,
                Description = "Despesa",
                Amount = 500
            }
        });

            db.SaveChanges();
        }

        // Act
        var loggerMock = new Mock<ILogger<EmailWorker>>();
        var worker = new EmailWorker(provider, loggerMock.Object);
        await worker.StartAsync(CancellationToken.None);

        // Assert
        emailMock.Verify(s => s.SendEmailAsync(It.IsAny<List<SendEmailDto>>()), Times.Never);
    }
    [Fact]
    public async Task Handle_ShouldCreateUserNotificationSuccessfully()
    {
        // Arrange
        var command = new CreateUserNotificationsCommand
        {
            Name = "Luan",
            Email = "luan@example.com",
            Hour = new TimeOnly(18, 0),
            IsActive = true
        };

        var expectedUser = new UserNotification
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Email = command.Email,
            Hour = command.Hour,
            IsActive = command.IsActive
        };

        var repositoryMock = new Mock<IUserNotificationRepository>();

        // Simula retorno do repositório
        repositoryMock
            .Setup(r => r.CreateAsync(It.IsAny<UserNotification>()))
            .ReturnsAsync(expectedUser);

        var handler = new CreateUserNotificationsCommandHandler(repositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Email.Should().Be(command.Email);
        result.Hour.Should().Be(command.Hour);
        result.IsActive.Should().Be(command.IsActive);

        repositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserNotification>()), Times.Once);
    }
    [Fact]
    public async Task DeleteUserNotification_ShouldReturnOk_WhenDeleteSucceeds()
    {
        // Arrange
        var command = new DeleteUserNotificationCommand
        {
            Id = Guid.NewGuid()
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteUserNotificationCommand>(c => c.Id == command.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUserNotification(command);

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var data = okResult.Value!.GetType().GetProperty("data")!.GetValue(okResult.Value);
        data.Should().Be(true);
    }

}
