﻿
namespace PD_FOOD.Domain.Entities
{
    public enum TransactionType { Income = 1, Expense = 2 }

    public class FinancialTransaction
    {
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

}
