using TM.Core.Enum;

namespace TM.Services.DTO
{
    public class ContextTaskAggregateDTO
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public int UserId { get; set; }
        public CurrentTaskStatus Status { get; set; }
        public int CountOfContextTask { get; set; }

        public ContextTaskAggregateDTO()
        {
            ProjectTitle = string.Empty;
        }
    }
}
