using MediatR;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Application.Queries.Transactions
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, PagedResult<FinancialTransaction>>
    {
        private readonly ITransactionRepository _repository;

        public GetAllTransactionsQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<FinancialTransaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request.Page, request.PageSize);
        }
    }

}
