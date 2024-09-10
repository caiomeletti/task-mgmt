namespace TM.Services.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UserId { get; set; }

        public virtual IEnumerable<ContextTaskDTO>? ContextTasks { get; set; }

        public ProjectDTO()
        {
            Title= string.Empty;
            Description = string.Empty;
            UpdateAt = DateTime.MinValue;
        }
    }
}
