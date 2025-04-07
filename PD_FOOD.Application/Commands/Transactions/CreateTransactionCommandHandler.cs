using MediatR;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Application.Commands.Transactions
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, FinancialTransaction>
    {
        private readonly ITransactionRepository _repository;

        public CreateTransactionCommandHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<FinancialTransaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Date = request.Date,
                Description = request.Description,
                Amount = request.Amount
            };

            return await _repository.CreateAsync(transaction);
        }
    }
}
