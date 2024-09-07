using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface IContextTaskRepository
    {
        Task<IEnumerable<ContextTask>?> GetAsync(int projectId);
        Task<ContextTask> CreateAsync(ContextTask contextTask);
    }
}