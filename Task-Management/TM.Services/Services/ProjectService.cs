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

        public ProjectService(
            IMapper mapper,
            IProjectRepository projectRepository
            )
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDTO> CreateProjectAsync(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            project.UpdateAt = DateTime.Now;
            var projectCreated = await _projectRepository.CreateAsync(project);

            return _mapper.Map<ProjectDTO>(projectCreated);
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectAsync()
        {
            return await GetProjectAsync(-1, -1);
        }

        public async Task<ProjectDTO?> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            return project != null
                ? _mapper.Map<ProjectDTO>(project)
                : null;
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectByUserIdAsync(int userId)
        {
            return await GetProjectAsync(-1, userId);
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectAsync(int projectId, int userId)
        {
            var projects = await _projectRepository.GetAsync(projectId, userId);
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
                //todo verificar se possui tasks pendentes
                //se possui tasks
                //return ResultDisabling.HasPendingTask

                var wasDisabled = await _projectRepository.DisableAsync(projectId);

                return wasDisabled
                    ? (byte)ResultDisabling.Disabled
                    : (byte)ResultDisabling.NotFound;
            }
        }
    }
}
