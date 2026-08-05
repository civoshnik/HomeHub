using Entity;

public class HouseholdBudget : BaseEntity
{
    public Guid HouseholdId { get; set; }

    public decimal Balance { get; set; }
    public decimal Rent { get; set; }
    public decimal Utilities { get; set; }

    public int Year { get; set; }
    public int Month { get; set; }
}