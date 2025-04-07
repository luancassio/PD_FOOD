using MediatR;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;

namespace PD_FOOD.Application.Queries.Transactions
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, FinancialTransaction>
    {
        private readonly ITransactionRepository _repository;
        public GetTransactionByIdQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public async Task<FinancialTransaction> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
            => await _repository.GetByIdAsync(request.Id);
    }

}
