using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TM.Core.Enum;

namespace TM.API.ViewModels.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterContextTaskReportModel : FilterModel
    {
        /// <summary>
        /// Status da atividade
        /// </summary>
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurrentTaskStatus Status { get; set; }

        /// <summary>
        /// Data inicial
        /// </summary>
        [Required] 
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FilterContextTaskReportModel() : base()
        {
        }
    }
}
