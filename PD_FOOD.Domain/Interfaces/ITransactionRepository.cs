using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;


namespace PD_FOOD.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<PagedResult<FinancialTransaction>> GetAllAsync(int page, int pageSize);
        Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction);
        Task<FinancialTransaction?> GetByIdAsync(Guid id);
        Task<List<DailyBalanceDto>> GetNegativeBalanceDaysAsync();
    }
}
