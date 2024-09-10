using TM.Core.Enum;

namespace TM.Services.DTO
{
    public class ContextTaskDTO
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

        public virtual IEnumerable<TaskCommentDTO>? TaskComments { get; set; }

        public ContextTaskDTO()
        {
            Title = string.Empty; 
            Description=string.Empty; 
            DueDate = DateTime.MinValue;
            UpdateAt = DateTime.MinValue;
        }
    }
}
