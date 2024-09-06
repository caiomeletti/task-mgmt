
using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateProjectAsync(Project project);
        Task<IEnumerable<Project>?> GetProjectAsync();
    }
}
