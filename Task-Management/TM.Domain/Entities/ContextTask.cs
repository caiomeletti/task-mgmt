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

        public ContextTask()
        {
            Title = string.Empty;
            Description = string.Empty;
            Priority = 0;
            Status = 0;
            UpdateAt = DateTime.Now;
            Enabled = true;
        }
    }
}
