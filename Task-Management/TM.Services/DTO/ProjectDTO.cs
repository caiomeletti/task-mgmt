namespace TM.Services.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UserId { get; set; }
    }
}
