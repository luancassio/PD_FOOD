using MediatR;
using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Application.Commands
{
    public class CreateTransactionCommand : IRequest<FinancialTransaction>
    {
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
