using MediatR;
using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Infrastructure;

namespace PD_FOOD.Application.Queries
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<FinancialTransaction>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllTransactionsQueryHandler(ApplicationDbContext context) => _context = context;
        public async Task<List<FinancialTransaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
            => await _context.FinancialTransactions.ToListAsync();
    }

}
