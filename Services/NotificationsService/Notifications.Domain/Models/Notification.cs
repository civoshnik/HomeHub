using Entity;
using Notifications.Domain.Enum;

namespace Notifications.Domain.Models
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public DateTimeOffset? SentAt { get; set; }
    }

}
