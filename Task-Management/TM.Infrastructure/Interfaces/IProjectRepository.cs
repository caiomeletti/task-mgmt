
using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateAsync(Project project);
        Task<bool> DisableAsync(int projectId);
        Task<Project?> GetAsync(int projectId);
        Task<IEnumerable<Project>?> GetAsync(int projectId, int userId);
    }
}
