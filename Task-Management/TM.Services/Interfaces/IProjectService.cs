
using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDTO> CreateProjectAsync(ProjectDTO projectDTO);
        Task<IEnumerable<ProjectDTO>> GetProjectAsync();
    }
}
