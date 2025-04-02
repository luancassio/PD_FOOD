using MediatR;
using PD_FOOD.Domain.Dtos;

namespace PD_FOOD.Application.Queries
{
    public record GetNegativeBalanceDaysQuery() : IRequest<List<DailyBalanceDto>>;
}
