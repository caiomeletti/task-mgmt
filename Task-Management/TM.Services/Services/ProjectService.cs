using AutoMapper;
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
            var projectCreated = await _projectRepository.CreateProjectAsync(project);

            return _mapper.Map<ProjectDTO>(projectCreated);
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectAsync()
        {
            return await GetProjectAsync(-1, -1);
        }

        public async Task<ProjectDTO?> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetProjectAsync(projectId);
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
            var projects = await _projectRepository.GetProjectAsync(projectId, userId);
            var projectsDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);

            return projectsDTO;
        }
    }
}
