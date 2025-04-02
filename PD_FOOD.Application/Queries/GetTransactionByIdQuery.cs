using MediatR;
using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Application.Queries
{
    public record GetTransactionByIdQuery(Guid Id) : IRequest<FinancialTransaction>;
}
