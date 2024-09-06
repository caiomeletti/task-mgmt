using AutoMapper;
using TM.Infrastructure.Interfaces;
using TM.Services.DTO;
using TM.Services.Interfaces;

namespace TM.Services.Services
{
    public class ContextTaskService : IContextTaskService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IContextTaskRepository _contextTaskRepository;

        public ContextTaskService(
            IMapper mapper,
            IProjectRepository projectRepository,
            IContextTaskRepository contextTaskRepository
            )
        {
            _mapper = mapper;
            _contextTaskRepository = contextTaskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDTO?> GetContextTaskAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            if (project != null)
            {
                project.ContextTasks = await _contextTaskRepository.GetAsync(projectId);
            }

            return project != null
                ? _mapper.Map<ProjectDTO>(project)
                : null;
        }

    }
}
