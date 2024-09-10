namespace TM.API.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CreateProjectViewModel()
        {
            Title = string.Empty;
            Description = string.Empty;
        }
    }
}
