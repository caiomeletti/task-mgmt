namespace TM.Domain.Entities
{
    public class TaskComment
    {
        public int Id { get; set; }
        public int ContextTaskId { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Enabled { get; set; }

        public TaskComment()
        {
            Comment = string.Empty;
            UpdateAt = DateTime.Now;
            Enabled = true;
        }
    }
}
