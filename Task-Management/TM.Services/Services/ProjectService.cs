using AutoMapper;
using TM.Core.Enum;
using TM.Core.Structs;
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

        public async Task<Result<ProjectDTO>> CreateProjectAsync(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            project.UpdateAt = DateTime.Now;

            Project? projectCreated = null;
            if (Validate(project))
                projectCreated = await _projectRepository.CreateAsync(project);

            return projectCreated != null
                ? new SuccessResult<ProjectDTO>(_mapper.Map<ProjectDTO>(projectCreated))
                : new ErrorResult<ProjectDTO>("Error creating the project");
        }

        private static bool Validate(Project project)
        {
            bool valid = !string.IsNullOrEmpty(project.Title);
            return valid;
        }

        public async Task<Result<IEnumerable<ProjectDTO>>> GetProjectAsync(bool includeTasks)
        {
            return await GetProjectAsync(-1, -1, includeTasks);
        }

        public async Task<Result<ProjectDTO>> GetProjectByIdAsync(int projectId, bool includeTasks)
        {
            var project = await _projectRepository.GetAsync(projectId);
            if (includeTasks && project != null)
            {
                project.ContextTasks = await _contextTaskRepository.GetAllAsync(projectId);
            }

            return project != null
                ? new SuccessResult<ProjectDTO>(_mapper.Map<ProjectDTO>(project))
                : new ErrorResult<ProjectDTO>("Project not found");
        }

        public async Task<Result<IEnumerable<ProjectDTO>>> GetProjectByUserIdAsync(int userId, bool includeTasks)
        {
            return await GetProjectAsync(-1, userId, includeTasks);
        }

        private async Task<Result<IEnumerable<ProjectDTO>>> GetProjectAsync(int projectId, int userId, bool includeTasks)
        {
            var projects = await _projectRepository.GetAsync(projectId, userId);
            if (includeTasks && projects != null && projects.Any())
            {
                foreach (var project in projects)
                {
                    project.ContextTasks = await _contextTaskRepository.GetAllAsync(project.Id);
                }
            }

            var projectsDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);

            return new SuccessResult<IEnumerable<ProjectDTO>>(projectsDTO);
        }

        public async Task<Result<bool>> DisableProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            if (project == null)
                return new NotFoundResult<bool>("Project not found");
            else
            {
                var contextTasks = await _contextTaskRepository.GetAllAsync(projectId);
                var pendingTasks = contextTasks != null && contextTasks.Any(x => x.Status == CurrentTaskStatus.Pending);
                if (pendingTasks)
                    return new ErrorResult<bool>("Project has pending tasks. Remove or complete tasks before disabling the project.");

                var wasDisabled = await _projectRepository.DisableAsync(projectId);

                return wasDisabled
                    ? new SuccessResult<bool>(wasDisabled)
                    : new NotFoundResult<bool>("Project not found");
            }
        }
    }
}
