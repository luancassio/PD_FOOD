using MediatR;
using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Infrastructure;

namespace PD_FOOD.Application.Queries
{
    public class GetNegativeBalanceDaysQueryHandler : IRequestHandler<GetNegativeBalanceDaysQuery, List<DailyBalanceDto>>
    {
        private readonly ApplicationDbContext _context;
        public GetNegativeBalanceDaysQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<List<DailyBalanceDto>> Handle(GetNegativeBalanceDaysQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _context.FinancialTransactions.ToListAsync();

            var result = transactions
                .GroupBy(t => t.Date.Date)
                .Select(g => new DailyBalanceDto
                {
                    Date = g.Key,
                    Income = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
                    Expense = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
                }).Where(r => r.IsNegative)
                .ToList();

            return result;
        }
    }

}
