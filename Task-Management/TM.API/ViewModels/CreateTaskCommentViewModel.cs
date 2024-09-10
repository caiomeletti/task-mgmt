namespace TM.API.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateTaskCommentViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CreateTaskCommentViewModel()
        {
            Comment = string.Empty;
        }
    }
}
