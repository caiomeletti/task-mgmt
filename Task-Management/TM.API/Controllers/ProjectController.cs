﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TM.API.ViewModels;
using TM.Core.Enum;
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
        private readonly IContextTaskService _contextTaskService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="projectService"></param>
        /// <param name="contextTaskService"></param>
        public ProjectController(
            IMapper mapper,
            IProjectService projectService,
            IContextTaskService contextTaskService)
        {
            _mapper = mapper;
            _projectService = projectService;
            _contextTaskService = contextTaskService;
        }

        /// <summary>
        /// Exibir lista de projetos
        /// </summary>
        /// <param name="includeTasks">Incluir as atividades do projeto</param>
        /// <returns></returns>
        /// <response code="200">Quando existir projetos a exibir</response>
        /// <response code="404">Quando não existir projetos a exibir</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectAsync([FromQuery] bool includeTasks = false)
        {
            var result = await _projectService.GetProjectAsync(includeTasks);
            return result != null && result.Any()
                ? Ok(result)
                : NotFound();
        }

        /// <summary>
        /// Obter um projeto
        /// </summary>
        /// <param name="id">Id do projeto</param>
        /// <param name="includeTasks">Incluir as atividades do projeto</param>
        /// <returns></returns>
        /// <response code="200">Quando o projeto for encontrado</response>
        /// <response code="404">Quando o projeto não for encontrado</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProjectDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectByIdAsync([FromRoute] int id, [FromQuery] bool includeTasks = false)
        {
            var result = await _projectService.GetProjectByIdAsync(projectId: id, includeTasks);
            return result != null
                ? Ok(result)
                : NotFound();
        }

        /// <summary>
        /// Exibir lista de projetos de um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="includeTasks">Incluir as atividades do projeto</param>
        /// <returns></returns>
        /// <response code="200">Quando existir projetos associados ao usuário</response>
        /// <response code="404">Quando não existir projetos associados ao usuário</response>
        [HttpGet]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ProjectDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectByUserIdAsync([FromRoute] int id, [FromQuery] bool includeTasks = false)
        {
            var result = await _projectService.GetProjectByUserIdAsync(userId: id, includeTasks);
            return result != null
                ? Ok(result)
                : NotFound();
        }

        /// <summary>
        /// Criar um projeto
        /// </summary>
        /// <param name="createProjectViewModel">Dados do projeto</param>
        /// <returns></returns>
        /// <response code="201">Quando o projeto for criado com sucesso</response>
        /// <response code="404">Quando ocorrwer falha ao criar o projeto</response>
        [HttpPost]
        [ProducesResponseType(typeof(ProjectDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectViewModel createProjectViewModel)
        {
            var projectDTO = _mapper.Map<ProjectDTO>(createProjectViewModel);
            var projectCreated = await _projectService.CreateProjectAsync(projectDTO);

            return projectCreated != null
                ? Created(Request.Path, projectCreated)
                : BadRequest();
        }

        /// <summary>
        /// Desativar um projeto
        /// </summary>
        /// <param name="id">Id do projeto</param>
        /// <remarks>123</remarks>
        /// <response code="202">Quando o projeto for desativado com sucesso</response>
        /// <response code="403">Quando o projeto ainda possuir atividades não concluídas</response>
        /// <response code="404">Quando o projeto não for encontrado</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DisableProjectByIdAsync([FromRoute] int id)
        {
            var result = await _projectService.DisableProjectByIdAsync(projectId: id);
            return (ResultDisabling)result == ResultDisabling.HasPendingTask
                ? Forbid()
                : (ResultDisabling)result == ResultDisabling.NotFound
                    ? NotFound()
                    : Accepted();
        }

        /// <summary>
        /// Exibir lista de tarefas de um projeto
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        /// <response code="200">Quando existir tarefas associadas ao projeto</response>
        /// <response code="404">Quando não existir tarefas associadas ao projeto</response>
        [HttpGet]
        [Route("{id}/tasks")]
        [ProducesResponseType(typeof(IEnumerable<ContextTaskDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTasksAsync([FromRoute] int id)
        {
            var result = await _contextTaskService.GetContextTaskAsync(projectId: id);
            return result != null
                ? Ok(result)
                : NotFound();
        }

        /// <summary>
        /// Criar uma atividade em um projeto
        /// </summary>
        /// <param name="id">Id do projeto</param>
        /// <param name="createContextTaskViewModel">Dados da atividade</param>
        /// <returns></returns>
        /// <response code="201">Quando a atividade for criada com sucesso</response>
        /// <response code="404">Quando o projeto não for encontrado</response>
        /// <response code="400">Quando ocorrer falha ao criar a atividade</response>
        [HttpPost]
        [Route("{id}/tasks")]
        [ProducesResponseType(typeof(ContextTaskDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateContextTaskAsync([FromRoute] int id, [FromBody] CreateContextTaskViewModel createContextTaskViewModel)
        {
            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(createContextTaskViewModel);
            contextTaskDTO.ProjectId = id;
            var contextTaskCreated = await _contextTaskService.CreateContextTaskAsync(contextTaskDTO);

            return contextTaskCreated != null
                ? Created(Request.Path, contextTaskCreated)
                : BadRequest();
        }

        /// <summary>
        /// Atualizar uma tarefa
        /// </summary>
        /// <param name="id">Id da tarefa</param>
        /// <param name="updateContextTaskViewModel">Dados da tarefa</param>
        /// <returns></returns>
        /// <response code="202">Quando a tarefa for alterada com sucesso</response>
        /// <response code="404">Quando a tarefa não for encontrada</response>
        [HttpPut]
        [Route("tasks/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateContextTaskAsync([FromRoute]int id, [FromBody] UpdateContextTaskViewModel updateContextTaskViewModel)
        {
            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(updateContextTaskViewModel);
            contextTaskDTO.Id = id;
            var contextTaskUpdated = await _contextTaskService.UpdateContextTaskAsync(contextTaskDTO);

            return contextTaskUpdated != null
                ? Ok(contextTaskUpdated)
                : BadRequest();
        }

        /// <summary>
        /// Desativar uma tarefa do projeto
        /// </summary>
        /// <param name="id">Id da tarefa</param>
        /// <remarks></remarks>
        /// <response code="202">Quando a tarefa for desativada com sucesso</response>
        /// <response code="404">Quando a tarefa não for encontrada</response>
        [HttpDelete]
        [Route("tasks/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DisableContextTaskByIdAsync([FromRoute] int id)
        {
            var result = await _contextTaskService.DisableContextTaskByIdAsync(contextTaskId: id);
            return result
                ? Accepted()
                : NotFound();
        }
    }
}
