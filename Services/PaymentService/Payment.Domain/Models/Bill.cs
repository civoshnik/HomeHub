using Entity;

namespace Payment.Domain.Models
{
    public class Bill : BaseEntity
    {
        public Guid HouseholdId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTimeOffset? PaidAt { get; set; }
    }
}
