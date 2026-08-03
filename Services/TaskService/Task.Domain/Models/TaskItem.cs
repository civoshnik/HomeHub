using Entity;

namespace Task.Domain.Models
{
    public class TaskItem : BaseEntity
    {
        public Guid HouseholdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? AssignedUserId { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
