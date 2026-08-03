using Entity;

namespace Expenses.Domain.Models
{
    public class Expense : BaseEntity
    {
        public Guid HouseholdId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTimeOffset OccurredAt { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
    }
}
