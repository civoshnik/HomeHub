using Entity;

namespace Expenses.Domain.Models
{
    public class Category : BaseEntity
    {
        public Guid HouseholdId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
    }
}
