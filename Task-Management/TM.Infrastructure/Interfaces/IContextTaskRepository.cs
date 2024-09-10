using TM.Core.Structs;
using TM.Domain.Entities;
using TM.Domain.Utilities.QueryParams;

namespace TM.Infrastructure.Interfaces
{
    public interface IContextTaskRepository
    {
        Task<ContextTask?> GetAsync(int contextTaskId);
        Task<IEnumerable<ContextTask>?> GetAllAsync(int projectId);
        Task<IEnumerable<ContextTaskAggregate>?> GetCountAsync(QueryParamsContextTaskReport query);
        Task<ContextTask> CreateAsync(ContextTask contextTask);
        Task<bool> DisableAsync(int contextTaskId);
        Task<ContextTask> UpdateAsync(ContextTask contextTask);
    }
}