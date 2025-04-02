using MediatR;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Infrastructure;

namespace PD_FOOD.Application.Queries
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, FinancialTransaction>
    {
        private readonly ApplicationDbContext _context;
        public GetTransactionByIdQueryHandler(ApplicationDbContext context) => _context = context;
        public async Task<FinancialTransaction> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
            => await _context.FinancialTransactions.FindAsync(request.Id);
    }

}
