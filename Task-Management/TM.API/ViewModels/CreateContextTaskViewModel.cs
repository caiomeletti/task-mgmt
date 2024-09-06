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
        public byte Priority { get; set; }
        public byte Status { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
