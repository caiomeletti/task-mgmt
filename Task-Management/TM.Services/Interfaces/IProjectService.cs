using TM.Core.Structs;
using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Result<ProjectDTO>> CreateProjectAsync(ProjectDTO projectDTO);
        Task<Result<IEnumerable<ProjectDTO>>> GetProjectAsync(bool includeTasks);
        Task<Result<ProjectDTO>> GetProjectByIdAsync(int projectId, bool includeTasks);
        Task<Result<IEnumerable<ProjectDTO>>> GetProjectByUserIdAsync(int userId, bool includeTasks);
        Task<Result<bool>> DisableProjectByIdAsync(int projectId);
    }
}
