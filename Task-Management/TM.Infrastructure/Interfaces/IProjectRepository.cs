
using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateProjectAsync(Project project);
        Task<Project?> GetProjectAsync(int projectId);
        Task<IEnumerable<Project>?> GetProjectAsync(int userId, int userId1);
    }
}
