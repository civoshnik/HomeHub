using Entity;

namespace Shopping.Domain.Models
{
    public class ShoppingList : BaseEntity
    {
        public Guid HouseholdId { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
