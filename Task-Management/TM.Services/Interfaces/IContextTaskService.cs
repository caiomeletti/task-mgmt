using TM.Core.Structs;
using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IContextTaskService
    {
        Task<Result<ContextTaskDTO>> CreateContextTaskAsync(ContextTaskDTO contextTaskDTO);
        Task<Result<bool>> DisableContextTaskByIdAsync(int contextTaskId);
        Task<Result<ProjectDTO>> GetContextTaskAsync(int projectId);
        Task<Result<ContextTaskDTO>> UpdateContextTaskAsync(ContextTaskDTO contextTaskDTO);
    }
}