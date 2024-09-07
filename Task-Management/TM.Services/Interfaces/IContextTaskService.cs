using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IContextTaskService
    {
        Task<ContextTaskDTO?> CreateContextTaskAsync(ContextTaskDTO contextTaskDTO);
        Task<bool> DisableContextTaskByIdAsync(int contextTaskId);
        Task<ProjectDTO?> GetContextTaskAsync(int projectId);
        Task<ContextTaskDTO?> UpdateContextTaskAsync(ContextTaskDTO contextTaskDTO);
    }
}