using Entity;

namespace Expenses.Domain.Models
{
    public class RecurringExpense : BaseEntity
    {
        public Guid HouseholdId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string CronExpression { get; set; }
        public DateTimeOffset NextExecution { get; set; }
    }
}
