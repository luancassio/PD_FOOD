using MediatR;
using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Application.Queries.Transactions
{
    public class GetNegativeBalanceDaysQueryHandler : IRequestHandler<GetNegativeBalanceDaysQuery, List<DailyBalanceDto>>
    {
        private readonly ITransactionRepository _repository;
        public GetNegativeBalanceDaysQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<DailyBalanceDto>> Handle(GetNegativeBalanceDaysQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetNegativeBalanceDaysAsync();
        }
    }

}
