namespace User.Domain.Models
{
    public class HouseholdMember
    {
        public Guid HouseholdId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
