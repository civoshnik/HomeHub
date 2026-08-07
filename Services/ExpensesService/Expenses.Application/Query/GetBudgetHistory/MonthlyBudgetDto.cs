using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Application.Query.GetBudgetHistory
{
    public record MonthlyBudgetDto(int Month,decimal TotalAmount);
}
