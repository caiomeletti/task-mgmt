using TM.Core.Enum;

namespace TM.Domain.Entities
{
    public class ContextTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public CurrentTaskStatus Status { get; set; }
        public int ProjectId { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UserId { get; set; }
        public bool Enabled { get; set; }

        public virtual IEnumerable<TaskComment>? TaskComments { get; set; }

        public ContextTask()
        {
            Title = string.Empty;
            Description = string.Empty;
            Priority = 0;
            Status = 0;
            UpdateAt = DateTime.Now;
            Enabled = true;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not ContextTask)
                return false;

            var other = (ContextTask)obj;
            if (Id != other.Id ||
                Title != other.Title ||
                Description != other.Description ||
                DueDate != other.DueDate ||
                Priority != other.Priority ||
                Status != other.Status ||
                ProjectId != other.ProjectId ||
                UpdateAt != other.UpdateAt ||
                UserId != other.UserId ||
                Enabled != other.Enabled)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^
                Title.GetHashCode() ^
                Description.GetHashCode() ^
                DueDate.GetHashCode() ^
                Priority.GetHashCode() ^
                Status.GetHashCode() ^
                ProjectId.GetHashCode() ^
                UpdateAt.GetHashCode() ^
                UserId.GetHashCode() ^
                Enabled.GetHashCode();
        }
    }
}
