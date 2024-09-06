using AutoMapper;
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

        public async Task<IList<ProjectDTO>> GetProjectAsync()
        {
            var allProjects = await _projectRepository.GetProjectAsync();
            var allProjectsDTO = _mapper.Map<IList<ProjectDTO>>(allProjects);

            return new List<ProjectDTO>(allProjectsDTO);
        }
    }
}
