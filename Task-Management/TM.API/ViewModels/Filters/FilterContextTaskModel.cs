using TM.Core.Enum;

namespace TM.API.ViewModels.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterContextTaskModel : FilterModel
    {
        /// <summary>
        /// 
        /// </summary>
        public CurrentTaskStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FilterContextTaskModel() : base()
        {
        }
    }
}
