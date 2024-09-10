using AutoMapper;
using Moq;
using TM.API.Utilities;
using TM.Core.Structs;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;
using TM.Services.DTO;
using TM.Services.Services;
using TM.UnitTest.Utilities;

namespace TM.UnitTest.Services
{
    public class ProjectServiceTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProjectRepository> _projectRepository;
        readonly Mock<IContextTaskRepository> _contextTaskRepository;

        readonly ProjectService _projectService;

        public ProjectServiceTest()
        {
            _mapper = ConfigAutoMapper.CreateMapper();

            _projectRepository = new Mock<IProjectRepository>();
            _contextTaskRepository = new Mock<IContextTaskRepository>();

            _projectRepository
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(EntityGenerator.GetProject());
            _projectRepository
                .Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(EntityGenerator.GetProject(2));
            _projectRepository
                .Setup(x => x.DisableAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            _projectRepository
                .Setup(x => x.CreateAsync(It.IsAny<Project>()))
                .ReturnsAsync(EntityGenerator.GetProject());

            _contextTaskRepository
                .Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .ReturnsAsync(EntityGenerator.GetContextTask(3));

            _projectService = new ProjectService(
                _mapper,
                _projectRepository.Object,
                _contextTaskRepository.Object);
        }

        [Fact(DisplayName = "Projeto não pode ser removido se ainda houver tarefas pendentes associadas a ele")]
        public async Task ProjectCannotBeRemovedIfThereAreStillPendingTasks()
        {
            var result = await _projectService.DisableProjectByIdAsync(1);

            Assert.True(result is ErrorResult<bool>);
        }

        [Fact(DisplayName = "Criar um projeto com dados válidos deve ocorrer com sucesso")]
        public async Task CreateProjectAsync()
        {
            var projectDTO = _mapper.Map<ProjectDTO>(EntityGenerator.GetCreateProjectViewModel());
            var result = await _projectService.CreateProjectAsync(projectDTO);

            Assert.True(result is SuccessResult<ProjectDTO>);
        }

        [Fact(DisplayName = "O título do projeto deve ser válido")]
        public async Task TheTitleOfTheProjectMustBeValid()
        {
            var projectDTO = _mapper.Map<ProjectDTO>(EntityGenerator.GetCreateProjectViewModel());
            projectDTO.Title = string.Empty;
            var result = await _projectService.CreateProjectAsync(projectDTO);

            Assert.True(result is ErrorResult<ProjectDTO>);
        }

        [Fact(DisplayName = "Requisição de projetos incluindo tarefas deve retornar com lista preenchida")]
        public async Task RequestProjectWithTasksMustBeReturnedWithFilledList()
        {
            var result = await _projectService.GetProjectAsync(true);

            Assert.True(result.Data.First().ContextTasks.Any());
        }

        [Fact(DisplayName = "Requisição de Id de projeto válido deve retornar o projeto")]
        public async Task RequestValidProjectIdMustBeReturnAProject()
        {
            var result = await _projectService.GetProjectByIdAsync(1, false);

            Assert.True(result.Data.Id > 0);
        }

        [Fact(DisplayName = "Requisição de projetos de um usuário válido deve retornar conteúdo")]
        public async Task RequestProjectsFromAValidUserMustReturnContent()
        {
            var result = await _projectService.GetProjectByUserIdAsync(1, false);

            Assert.True(result.Data.Any());
        }
    }
}
