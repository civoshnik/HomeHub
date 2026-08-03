using Entity;

namespace User.Domain.Models
{
    public class UserProfile : BaseEntity
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid HouseholdId { get; set; }
    }
}
