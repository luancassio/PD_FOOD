using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PD_FOOD.Application.Commands;
using PD_FOOD.Application.Queries;
using PD_FOOD.Controllers;
using System.Net;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Dtos;
using Microsoft.Extensions.Logging;

namespace PD_FOOD.Test.Controller
{
    public class TransactionsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TransactionsController _controller;
        private readonly Mock<ILogger<TransactionsController>> _loggerMock;

        public TransactionsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<TransactionsController>>();
            _controller = new TransactionsController(_mediatorMock.Object, _loggerMock.Object);
        }


        [Fact]
        public async Task Create_ShouldReturnCreatedWithTransaction_WhenCommandIsValid()
        {
            var command = new CreateTransactionCommand
            {
                Type = TransactionType.Income,
                Date = DateTime.UtcNow,
                Description = "Venda online",
                Amount = 1500.00m
            };

            var expectedTransaction = new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                Type = command.Type,
                Date = command.Date,
                Description = command.Description,
                Amount = command.Amount
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTransaction);

            var result = await _controller.Create(command);
            var response = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode ?? 200);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithList()
        {
            var expectedList = new List<FinancialTransaction>
        {
            new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.Income,
                Date = DateTime.UtcNow,
                Description = "Teste A",
                Amount = 500
            }
        };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllTransactionsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedList);

            var result = await _controller.GetAll();
            var response = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, response.StatusCode ?? 200);
        }

        [Fact]
        public async Task GetById_ShouldReturnTransaction_WhenExists()
        {
            var id = Guid.NewGuid();
            var transaction = new FinancialTransaction
            {
                Id = id,
                Type = TransactionType.Income,
                Date = DateTime.UtcNow,
                Description = "Teste",
                Amount = 100
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetTransactionByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transaction);

            var result = await _controller.GetById(id);
            var response = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, response.StatusCode ?? 200);
        }

        [Fact]
        public async Task GetNegativeBalanceDays_ShouldReturnOkWithDates()
        {
            var balances = new List<DailyBalanceDto>
        {
            new DailyBalanceDto
            {
                Date = DateTime.Today.AddDays(-1),
                Income = 100,
                Expense = 300
            }
        };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetNegativeBalanceDaysQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(balances);

            var result = await _controller.GetNegativeBalanceDays();
            var response = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode ?? 200);
        }
    }
}
