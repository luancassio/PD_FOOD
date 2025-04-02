using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD_FOOD.Domain.Dtos
{
    public class DailyBalanceDto
    {
        public DateTime Date { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance => Income - Expense;
        public bool IsNegative => Balance < 0;
    }
}
