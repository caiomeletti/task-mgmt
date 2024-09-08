using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface ITaskCommentRepository
    {
        Task<IEnumerable<TaskComment>?> GetAllAsync(int contextTaskId);
        Task<TaskComment> CreateAsync(TaskComment taskComment);
        Task<TaskComment> UpdateAsync(TaskComment taskComment);
    }
}
