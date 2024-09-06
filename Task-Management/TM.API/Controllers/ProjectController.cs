using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TM.API.ViewModels;
using TM.Services.DTO;
using TM.Services.Interfaces;

namespace TM.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/projects")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="projectService"></param>
        public ProjectController(
            IMapper mapper,
            IProjectService projectService)
        {
            _mapper = mapper;
            _projectService = projectService;
        }

        /// <summary>
        /// Exibir lista de projetos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectAsync()
        {
            var result = await _projectService.GetProjectAsync();
            return result != null
                ? Ok(result)
                : NotFound();
        }

        /// <summary>
        /// Obter um projeto
        /// </summary>
        /// <param name="id">Id do projeto</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProjectDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectByIdAsync([FromRoute] int id)
        {
            var result = await _projectService.GetProjectByIdAsync(projectId: id);
            return result != null
                ? Ok(result)
                : NotFound();
        }

        /// <summary>
        /// Exibir lista de projetos de um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ProjectDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectByUserIdAsync([FromRoute] int id)
        {
            var result = await _projectService.GetProjectByUserIdAsync(userId: id);
            return result != null
                ? Ok(result)
                : NotFound();
        }

        /// <summary>
        /// Criar um projeto
        /// </summary>
        /// <param name="createProjectViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProjectDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectViewModel createProjectViewModel)
        {
            var projectDTO = _mapper.Map<ProjectDTO>(createProjectViewModel);
            var projectCreated = await _projectService.CreateProjectAsync(projectDTO);

            return projectCreated != null
                ? Ok(projectCreated)
                : BadRequest();
        }
    }
}
