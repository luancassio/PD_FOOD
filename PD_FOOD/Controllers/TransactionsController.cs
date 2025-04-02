using MediatR;
using Microsoft.AspNetCore.Mvc;
using PD_FOOD.Application.Commands;
using PD_FOOD.Application.Queries;
using System.Net;

namespace PD_FOOD.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : MainController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(IMediator mediator, ILogger<TransactionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionCommand command)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateTransactionCommand: {@Command}", command);
                return CustomResponse(ModelState);
            }

            _logger.LogInformation("Creating a new transaction: {@Command}", command);

            var result = await _mediator.Send(command);

            _logger.LogInformation("Transaction created with ID: {TransactionId}", result.Id);

            return CustomResponse(new { status = HttpStatusCode.Created, data = result });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all transactions...");
            var result = await _mediator.Send(new GetAllTransactionsQuery());
            _logger.LogInformation("Returned {Count} transactions", result.Count);
            return CustomResponse(new { status = HttpStatusCode.OK, data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Requesting transaction by ID: {TransactionId}", id);

            var result = await _mediator.Send(new GetTransactionByIdQuery(id));

            if (result == null)
            {
                _logger.LogWarning("Transaction not found for ID: {TransactionId}", id);
                return NotFound();
            }

            _logger.LogInformation("Transaction found: {TransactionId}", id);
            return CustomResponse(result);
        }

        [HttpGet("negative-balance-days")]
        public async Task<IActionResult> GetNegativeBalanceDays()
        {
            _logger.LogInformation("Requesting negative balance days...");

            var result = await _mediator.Send(new GetNegativeBalanceDaysQuery());

            _logger.LogInformation("Returned {Count} days with negative balance", result.Count);

            return CustomResponse(new { status = HttpStatusCode.OK, data = result });
        }

    }
}
