
using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IList<ProjectDTO>> GetProjectAsync();
    }
}
