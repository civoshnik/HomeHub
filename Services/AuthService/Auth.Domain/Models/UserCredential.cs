using Auth.Domain.Enum;
using Entity;

namespace Auth.Domain.Models
{
    public class UserCredential : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public DateTimeOffset? LastAuthorizedAt { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
