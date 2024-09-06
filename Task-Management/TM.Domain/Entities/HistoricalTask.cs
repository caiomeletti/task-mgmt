namespace TM.Domain.Entities
{
    public class HistoricalTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public byte Priority { get; set; }
        public byte Status { get; set; }
        public int ContextTaskId { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UserId { get; set; }

        public HistoricalTask()
        {
            Title = string.Empty;
            Description = string.Empty;
            Priority = 0;
            Status = 0;
            UpdateAt = DateTime.Now;
        }
    }
}
