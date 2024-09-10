using TM.API.ViewModels.Filters;
using TM.Domain.Utilities.QueryParams;

namespace TM.API.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateQueryParams
    {
        internal static QueryParamsContextTaskReport ForContextTaskReport(FilterContextTaskReportModel filter)
        {
            return new QueryParamsContextTaskReport
            {
                Status = filter.Status,
                StartDate = filter.StartDate,
            };
        }
    }
}
