namespace User.Domain.Models
{
    public class HouseholdMember
    {
        public Guid HouseholdId { get; private set; }
        public Guid UserId { get; private set; }

        public string Role { get; private set; }
    }
}
