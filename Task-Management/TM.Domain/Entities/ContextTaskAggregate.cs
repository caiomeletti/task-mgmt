using TM.Core.Enum;

namespace TM.Domain.Entities
{
    public class ContextTaskAggregate
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public int UserId { get; set; }
        public CurrentTaskStatus Status { get; set; }
        public int CountOfContextTask { get; set; }

        public ContextTaskAggregate()
        {
            ProjectTitle = string.Empty;
        }
    }
}
