using AutoMapper;
using Microsoft.Extensions.Configuration;
using TM.Core.Enum;
using TM.Core.Structs;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;
using TM.Services.DTO;
using TM.Services.Interfaces;

namespace TM.Services.Services
{
    public class ContextTaskService : IContextTaskService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IContextTaskRepository _contextTaskRepository;
        private readonly ITaskCommentRepository _taskCommentRepository;
        private readonly IHistoricalTaskRepository _historicalTaskRepository;

        public ContextTaskService(
            IConfiguration configuration,
            IMapper mapper,
            IProjectRepository projectRepository,
            IContextTaskRepository contextTaskRepository,
            ITaskCommentRepository taskCommentRepository,
            IHistoricalTaskRepository historicalTaskRepository
            )
        {
            _configuration = configuration;
            _mapper = mapper;
            _contextTaskRepository = contextTaskRepository;
            _projectRepository = projectRepository;
            _taskCommentRepository = taskCommentRepository;
            _historicalTaskRepository = historicalTaskRepository;
        }

        public async Task<Result<ContextTaskDTO>> CreateContextTaskAsync(ContextTaskDTO contextTaskDTO)
        {
            Result<ContextTaskDTO> ret = new NotFoundResult<ContextTaskDTO>("Project not found");

            if (!int.TryParse(_configuration["ContextTask:MaxNumber"], out int maxNumberTasks))
                maxNumberTasks = 20;

            var contextTask = _mapper.Map<ContextTask>(contextTaskDTO);
            contextTask.UpdateAt = DateTime.Now;

            if (Validate(contextTask, out string message))
            {
                var project = await _projectRepository.GetAsync(contextTask.ProjectId);
                if (project != null)
                {
                    var contextTasks = await _contextTaskRepository.GetAllAsync(contextTask.ProjectId);
                    var currentNumberTasks = contextTasks != null
                        ? contextTasks.Count()
                        : 0;
                    if (currentNumberTasks < maxNumberTasks)
                    {
                        ContextTask? contextTaskCreated = await _contextTaskRepository.CreateAsync(contextTask);
                        ret = contextTaskCreated != null
                            ? new SuccessResult<ContextTaskDTO>(_mapper.Map<ContextTaskDTO>(contextTaskCreated))
                            : new ErrorResult<ContextTaskDTO>("Error creating the task");
                    }
                    else
                    {
                        ret = new ForbiddenResult<ContextTaskDTO>("Maximum number of tasks has been reached");
                    }
                }
                else
                {
                    ret = new NotFoundResult<ContextTaskDTO>("Project not found");
                }
            }
            else
            {
                ret = new ErrorResult<ContextTaskDTO>(message);
            }

            return ret;
        }

        private static bool Validate(ContextTask contextTask, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(contextTask.Title))
            {
                message = "The title of the task cannot be empty";
            }
            else if (contextTask.DueDate.CompareTo(DateTime.Today) <= 0)
            {
                message = "Due Date must be greater than the current date";
            }
            else if (contextTask.ProjectId <= 0)
            {
                message = "The ProjectId of the task cannot be empty";
            }

            return string.IsNullOrEmpty(message);
        }

        public async Task<Result<TaskCommentDTO>> CreateTaskCommentAsync(TaskCommentDTO taskCommentDTO)
        {
            Result<TaskCommentDTO> ret = new NotFoundResult<TaskCommentDTO>("Task not found");

            var taskComment = _mapper.Map<TaskComment>(taskCommentDTO);
            taskComment.UpdateAt = DateTime.Now;
            taskComment.Enabled = true;
            var contextTask = await _contextTaskRepository.GetAsync(taskComment.ContextTaskId);
            if (contextTask != null)
            {
                TaskComment? taskCommentCreated = await _taskCommentRepository.CreateAsync(taskComment);
                ret = taskCommentCreated != null
                    ? new SuccessResult<TaskCommentDTO>(_mapper.Map<TaskCommentDTO>(taskCommentCreated))
                    : new ErrorResult<TaskCommentDTO>("Error creating the comment");
            }

            return ret;
        }

        public async Task<Result<bool>> DisableContextTaskByIdAsync(int contextTaskId)
        {
            var ret = await _contextTaskRepository.DisableAsync(contextTaskId);
            return ret
                ? new SuccessResult<bool>(ret)
                : new NotFoundResult<bool>("Task not found");
        }

        public async Task<Result<ProjectDTO>> GetContextTaskAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            if (project != null)
            {
                project.ContextTasks = await _contextTaskRepository.GetAllAsync(projectId);

                if (project.ContextTasks != null && project.ContextTasks.Any())
                {
                    foreach (var task in project.ContextTasks)
                    {
                        task.TaskComments = await _taskCommentRepository.GetAllAsync(task.Id);
                    }
                }
            }

            return project != null
                ? new SuccessResult<ProjectDTO>(_mapper.Map<ProjectDTO>(project))
                : new NotFoundResult<ProjectDTO>("Task not found");
        }

        public async Task<Result<ContextTaskDTO>> UpdateContextTaskAsync(ContextTaskDTO contextTaskDTO)
        {
            ContextTask? contextTaskUpdated = null;

            var contextTask = _mapper.Map<ContextTask>(contextTaskDTO);
            var currentContextTask = await _contextTaskRepository.GetAsync(contextTask.Id);
            if (currentContextTask != null)
            {
                contextTask.UpdateAt = currentContextTask.UpdateAt;
                contextTask.ProjectId = currentContextTask.ProjectId;

                if (!PriorityMustBeEqual(contextTask.Priority, currentContextTask.Priority))
                    return new ErrorResult<ContextTaskDTO>("It is not allowed to change the priority of a task after it has been created");

                if (!currentContextTask.Equals(contextTask))
                {
                    contextTask.UpdateAt = DateTime.Now;
                    contextTaskUpdated = await _contextTaskRepository.UpdateAsync(contextTask);
                    if (contextTaskUpdated != null)
                    {
                        var historicalTask = _mapper.Map<HistoricalTask>(currentContextTask);
                        historicalTask.ContextTaskId = currentContextTask.Id;

                        _ = await _historicalTaskRepository.CreateAsync(historicalTask);
                    }
                }
                else
                {
                    contextTaskUpdated = contextTask;
                }
            }

            return contextTaskUpdated != null
                ? new SuccessResult<ContextTaskDTO>(_mapper.Map<ContextTaskDTO>(contextTaskUpdated))
                : new ErrorResult<ContextTaskDTO>("Error updating the task");
        }

        private bool PriorityMustBeEqual(Priority changePriority, Priority currentPriority)
        {
            return changePriority == currentPriority;
        }

        public async Task<Result<TaskCommentDTO>> UpdateTaskComentAsync(TaskCommentDTO taskCommentDTO)
        {
            TaskComment? taskCommentUpdated = null;

            var taskComment = _mapper.Map<TaskComment>(taskCommentDTO);
            var currentContextTask = await _taskCommentRepository.GetAsync(taskComment.Id);
            if (currentContextTask != null)
            {
                taskComment.UpdateAt = DateTime.Now;
                taskComment.ContextTaskId = currentContextTask.ContextTaskId;
                taskComment.Enabled = currentContextTask.Enabled;

                taskCommentUpdated = await _taskCommentRepository.UpdateAsync(taskComment);
            }

            return taskCommentUpdated != null
                ? new SuccessResult<TaskCommentDTO>(_mapper.Map<TaskCommentDTO>(taskCommentUpdated))
                : new ErrorResult<TaskCommentDTO>("Error updating comment");
        }
    }
}
