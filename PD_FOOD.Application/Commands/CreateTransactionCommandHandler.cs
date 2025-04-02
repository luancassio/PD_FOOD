using MediatR;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Infrastructure;

namespace PD_FOOD.Application.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, FinancialTransaction>
    {
        private readonly ApplicationDbContext _context;

        public CreateTransactionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
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
            _context.FinancialTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
