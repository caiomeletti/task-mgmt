using TM.Core.Enum;

namespace TM.Domain.Utilities.QueryParams
{
    public class QueryParamsContextTaskReport
    {
        public CurrentTaskStatus Status { get; set; }
        public DateTime StartDate { get; set; }
    }
}
