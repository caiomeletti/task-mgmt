using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDTO> CreateProjectAsync(ProjectDTO projectDTO);
        Task<IEnumerable<ProjectDTO>> GetProjectAsync();
        Task<ProjectDTO?> GetProjectByIdAsync(int projectId);
        Task<IEnumerable<ProjectDTO>> GetProjectAsync(int projectId, int userId);
        Task<IEnumerable<ProjectDTO>> GetProjectByUserIdAsync(int userId);
    }
}
