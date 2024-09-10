using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Moq;
using Mysqlx.Crud;
using System.Threading.Tasks;
using TM.API.Utilities;
using TM.API.ViewModels;
using TM.Core.Enum;
using TM.Core.Structs;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;
using TM.Services.DTO;
using TM.Services.Services;
using TM.UnitTest.Utilities;

namespace TM.UnitTest.Services
{
    public class ContextTaskServiceTest
    {
        Mock<IConfiguration> _configuration;
        private readonly IMapper _mapper;
        Mock<IProjectRepository> _projectRepository;
        Mock<IContextTaskRepository> _contextTaskRepository;
        Mock<ITaskCommentRepository> _taskCommentRepository;
        Mock<IHistoricalTaskRepository> _historicalTaskRepository;

        ContextTaskService _contextTaskService;

        public ContextTaskServiceTest()
        {
            _mapper = ConfigAutoMapper.CreateMapper();

            _projectRepository = new Mock<IProjectRepository>();
            _configuration = new Mock<IConfiguration>();
            _contextTaskRepository = new Mock<IContextTaskRepository>();
            _taskCommentRepository = new Mock<ITaskCommentRepository>();
            _historicalTaskRepository = new Mock<IHistoricalTaskRepository>();

            _projectRepository
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(EntityGenerator.GetProject());

            var contextTask = EntityGenerator.GetContextTask();

            contextTask.Priority = Priority.High;
            _contextTaskRepository
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(contextTask);
            _contextTaskRepository
                .Setup(x => x.UpdateAsync(It.IsAny<ContextTask>()))
                .ReturnsAsync(contextTask);
            _contextTaskRepository
                .Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .ReturnsAsync(EntityGenerator.GetContextTask(20));
            _contextTaskRepository
                .Setup(x => x.DisableAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _contextTaskService = new ContextTaskService(
                _configuration.Object,
                _mapper,
                _projectRepository.Object,
                _contextTaskRepository.Object,
                _taskCommentRepository.Object,
                _historicalTaskRepository.Object);
        }

        /// <summary>
        /// Cada tarefa deve ter uma prioridade atribuída
        /// </summary>
        /// <remarks>Devido a prioridade ter sido criada como enumerador, mesmo que o objeto não seja instanciado com um valor para esse atributo, o construtor vazio 'força' a atribuição do valor default do enum</remarks>
        [Fact(DisplayName = "Cada tarefa deve ter uma prioridade atribuída")]
        public void EachTaskMustHavePriorityAssigned()
        {
            var createContextTaskViewModel = EntityGenerator.GetCreateContextTaskViewModel();

            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(createContextTaskViewModel);
            var value = contextTaskDTO.Priority == Priority.High || contextTaskDTO.Priority == Priority.Medium || contextTaskDTO.Priority == Priority.Low;
            Assert.True(value);
        }

        /// <summary>
        /// Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada
        /// </summary>
        [Fact(DisplayName = "Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada")]
        public async Task ItIsNotAllowedToChangeThePriorityOfTaskAfterItHasBeenCreated()
        {
            var createContextTaskViewModel = EntityGenerator.GetCreateContextTaskViewModel();
            createContextTaskViewModel.Priority = Priority.Low;

            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(createContextTaskViewModel);
            contextTaskDTO.ProjectId = 1;
            contextTaskDTO.Id = 1;

            var result = await _contextTaskService.UpdateContextTaskAsync(contextTaskDTO);

            Assert.True(result is ErrorResult<ContextTaskDTO>);
        }

        [Fact(DisplayName = "Cada projeto deve ter um limite máximo de 20 tarefas.")]
        public async Task EachProjectHasAMaximumLimitOf20Tasks()
        {
            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(EntityGenerator.GetCreateContextTaskViewModel());
            contextTaskDTO.ProjectId = 1;

            var result = await _contextTaskService.CreateContextTaskAsync(contextTaskDTO);

            Assert.True(result is ForbiddenResult<ContextTaskDTO>);
        }

        [Fact(DisplayName = "Requisitar um projeto que contém tarefas deve retornar uma lista de tarefas")]
        public async Task RequestProjectThatContainsTasksMustBeReturnListOfTasks()
        {
            var result = await _contextTaskService.GetContextTaskAsync(1);

            Assert.True(result.Data.ContextTasks.Any());
        }

        [Fact(DisplayName = "Desabilitar uma tarefa válida deve ocorrer com sucesso")]
        public async Task DisablingAValidTaskMustBeSuccessfully()
        {
            var result = await _contextTaskService.DisableContextTaskByIdAsync(1);

            Assert.True(result is SuccessResult<bool>);
        }

        [Fact(DisplayName = "Título da tarefa deve estar preenchido")]
        public async Task TitleTaskMustBeFilled()
        {
            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(EntityGenerator.GetCreateContextTaskViewModel());
            contextTaskDTO.Title = string.Empty;

            var result = await _contextTaskService.CreateContextTaskAsync(contextTaskDTO);

            Assert.True(result is ErrorResult<ContextTaskDTO>);
        }

        [Fact(DisplayName = "Ao criar a tarefa a data de vencimento deve ser maior que a data atual")]
        public async Task WhenCreatingTheTaskTheDueDateMustBeGreaterThanTheCurrentDate()
        {
            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(EntityGenerator.GetCreateContextTaskViewModel());
            contextTaskDTO.DueDate = DateTime.Today.AddDays(-1);

            var result = await _contextTaskService.CreateContextTaskAsync(contextTaskDTO);

            Assert.True(result is ErrorResult<ContextTaskDTO>);
        }

        [Fact(DisplayName = "Ao criar a tarefa o ProjectId deve estar preenchido")]
        public async Task WhenCreatingTheTaskProjectIdMustBeFilled()
        {
            var contextTaskDTO = _mapper.Map<ContextTaskDTO>(EntityGenerator.GetCreateContextTaskViewModel());
            contextTaskDTO.ProjectId = 0;

            var result = await _contextTaskService.CreateContextTaskAsync(contextTaskDTO);

            Assert.True(result is ErrorResult<ContextTaskDTO>);
        }
    }
}
