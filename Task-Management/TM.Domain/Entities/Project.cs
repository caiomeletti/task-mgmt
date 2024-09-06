namespace TM.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UserId { get; set; }


        public Project()
        {
            Title = string.Empty;
            Description = string.Empty;
            UpdateAt = DateTime.Now;
        }
    }
}
