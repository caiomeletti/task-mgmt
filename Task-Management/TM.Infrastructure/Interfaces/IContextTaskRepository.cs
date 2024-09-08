using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface IContextTaskRepository
    {
        Task<ContextTask?> GetAsync(int contextTaskId);
        Task<IEnumerable<ContextTask>?> GetAllAsync(int projectId);
        Task<ContextTask> CreateAsync(ContextTask contextTask);
        Task<bool> DisableAsync(int contextTaskId);
        Task<ContextTask> UpdateAsync(ContextTask contextTask);
    }
}