using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction)
        {
            _context.FinancialTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<FinancialTransaction?> GetByIdAsync(Guid id)
        {
            return await _context.FinancialTransactions.FindAsync(id);
        }

        public async Task<PagedResult<FinancialTransaction>> GetAllAsync(int page, int pageSize)
        {
            var query = _context.FinancialTransactions.AsNoTracking().OrderByDescending(t => t.Date);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<FinancialTransaction>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<List<DailyBalanceDto>> GetNegativeBalanceDaysAsync()
        {
            var transactions = await _context.FinancialTransactions.AsNoTracking().ToListAsync();

            return transactions
                .GroupBy(t => t.Date.Date)
                .Select(g => new DailyBalanceDto
                {
                    Date = g.Key,
                    Income = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
                    Expense = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
                }).Where(r => r.IsNegative)
                .ToList();
        }
    }
}
