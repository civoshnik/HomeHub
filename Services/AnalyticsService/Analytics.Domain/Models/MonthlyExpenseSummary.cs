namespace Analytics.Domain.Models
{
    public class MonthlyExpenseSummary
    {
        public Guid HouseholdId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
