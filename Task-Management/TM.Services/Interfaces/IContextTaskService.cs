using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IContextTaskService
    {
        Task<ContextTaskDTO?> CreateContextTaskAsync(ContextTaskDTO contextTaskDTO);
        Task<ProjectDTO?> GetContextTaskAsync(int projectId);
    }
}