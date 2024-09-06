
using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>?> GetProjectAsync();
    }
}
