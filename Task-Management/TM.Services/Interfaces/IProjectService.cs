using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDTO> CreateProjectAsync(ProjectDTO projectDTO);
        Task<IEnumerable<ProjectDTO>> GetProjectAsync(bool includeTasks);
        Task<ProjectDTO?> GetProjectByIdAsync(int projectId, bool includeTasks);
        Task<IEnumerable<ProjectDTO>> GetProjectAsync(int projectId, int userId, bool includeTasks);
        Task<IEnumerable<ProjectDTO>> GetProjectByUserIdAsync(int userId, bool includeTasks);
        Task<byte> DisableProjectByIdAsync(int projectId);
    }
}
