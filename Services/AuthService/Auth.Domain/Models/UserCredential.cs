using Entity;

namespace Auth.Domain.Models
{
    public class UserCredential : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTimeOffset? LastAuthorizedAt { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
