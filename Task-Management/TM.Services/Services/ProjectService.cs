using AutoMapper;
using TM.Core.Enum;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;
using TM.Services.DTO;
using TM.Services.Interfaces;

namespace TM.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IContextTaskRepository _contextTaskRepository;

        public ProjectService(
            IMapper mapper,
            IProjectRepository projectRepository,
            IContextTaskRepository contextTaskRepository
            )
        {
            _mapper = mapper;
            _contextTaskRepository = contextTaskRepository;
            _projectRepository = projectRepository;
        }


        public async Task<ProjectDTO> CreateProjectAsync(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            project.UpdateAt = DateTime.Now;
            var projectCreated = await _projectRepository.CreateAsync(project);

            return _mapper.Map<ProjectDTO>(projectCreated);
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectAsync(bool includeTasks)
        {
            return await GetProjectAsync(-1, -1, includeTasks);
        }

        public async Task<ProjectDTO?> GetProjectByIdAsync(int projectId, bool includeTasks)
        {
            var project = await _projectRepository.GetAsync(projectId);
            if (includeTasks && project != null)
            {
                project.ContextTasks = await _contextTaskRepository.GetAsync(projectId);
            }

            return project != null
                ? _mapper.Map<ProjectDTO>(project)
                : null;
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectByUserIdAsync(int userId, bool includeTasks)
        {
            return await GetProjectAsync(-1, userId, includeTasks);
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectAsync(int projectId, int userId, bool includeTasks)
        {
            var projects = await _projectRepository.GetAsync(projectId, userId);
            if (includeTasks && projects != null && projects.Any())
            {
                foreach (var project in projects)
                {
                    project.ContextTasks = await _contextTaskRepository.GetAsync(project.Id);
                }
            }

            var projectsDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);

            return projectsDTO;
        }

        public async Task<byte> DisableProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            if (project == null)
                return (byte)ResultDisabling.NotFound;
            else
            {
                var contextTasks = await _contextTaskRepository.GetAsync(projectId);
                var pendingTasks = contextTasks != null && contextTasks.Any(x => x.Status == CurrentTaskStatus.Pending);
                if (pendingTasks)
                    return (byte)ResultDisabling.HasPendingTask;

                var wasDisabled = await _projectRepository.DisableAsync(projectId);

                return wasDisabled
                    ? (byte)ResultDisabling.Disabled
                    : (byte)ResultDisabling.NotFound;
            }
        }
    }
}
