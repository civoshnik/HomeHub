using Entity;

namespace Expenses.Domain.Models;

public class HouseholdBudgetCategory : BaseEntity
{
    public Guid HouseholdBudgetId { get; set; }

    public string Name { get; set; }
    public decimal Amount { get; set; }
}