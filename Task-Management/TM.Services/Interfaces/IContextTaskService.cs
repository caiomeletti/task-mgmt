using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IContextTaskService
    {
        Task<ProjectDTO?> GetContextTaskAsync(int projectId);
    }
}