using Microsoft.AspNetCore.Mvc;
using TM.Services.Interfaces;

namespace TM.API.Controllers
{
    [Route("api/v1/projects")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(
            IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectAsync()
        {
            var result = await _projectService.GetProjectAsync();
            return result != null
                ? Ok(result)
                : NotFound();
        }
    }
}
