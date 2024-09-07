using TM.Core.Enum;

namespace TM.API.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateContextTaskViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public CurrentTaskStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
