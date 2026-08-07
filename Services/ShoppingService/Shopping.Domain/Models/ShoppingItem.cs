using Entity;

namespace Shopping.Domain.Models
{
    public class ShoppingItem : BaseEntity
    {
        public Guid ShoppingListId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public bool IsPurchased { get; set; }
        public string StoreSection { get; set; }
    }
}
