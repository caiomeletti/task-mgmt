﻿using TM.Core.Structs;
using TM.Domain.Utilities.QueryParams;
using TM.Services.DTO;

namespace TM.Services.Interfaces
{
    public interface IContextTaskService
    {
        Task<Result<ContextTaskDTO>> CreateContextTaskAsync(ContextTaskDTO contextTaskDTO);
        Task<Result<TaskCommentDTO>> CreateTaskCommentAsync(TaskCommentDTO taskCommentDTO);
        Task<Result<bool>> DisableContextTaskByIdAsync(int contextTaskId);
        Task<Result<ProjectDTO>> GetContextTaskAsync(int projectId);
        Task<Result<IEnumerable<ContextTaskAggregateDTO>>> GetContextTaskReportAsync(QueryParamsContextTaskReport query, string requestingUser);
        Task<Result<ContextTaskDTO>> UpdateContextTaskAsync(ContextTaskDTO contextTaskDTO);
        Task<Result<TaskCommentDTO>> UpdateTaskComentAsync(TaskCommentDTO taskCommentDTO);
    }
}