using MediatR;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Application.Queries.Transactions
{
    public class GetAllTransactionsQuery : IRequest<PagedResult<FinancialTransaction>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
