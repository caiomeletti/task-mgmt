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
    }
}
