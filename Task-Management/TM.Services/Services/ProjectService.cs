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
            var allProjects = await _projectRepository.GetProjectAsync();
            var allProjectsDTO = _mapper.Map<IEnumerable<ProjectDTO>>(allProjects);

            return allProjectsDTO;
        }
    }
}
