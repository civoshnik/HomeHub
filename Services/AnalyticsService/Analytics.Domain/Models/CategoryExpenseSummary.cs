namespace Analytics.Domain.Models
{
    public class CategoryExpenseSummary
    {
        public Guid HouseholdId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
