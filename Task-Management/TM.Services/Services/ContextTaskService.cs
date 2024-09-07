﻿using AutoMapper;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;
using TM.Services.DTO;
using TM.Services.Interfaces;

namespace TM.Services.Services
{
    public class ContextTaskService : IContextTaskService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IContextTaskRepository _contextTaskRepository;
        private readonly IHistoricalTaskRepository _historicalTaskRepository;

        public ContextTaskService(
            IMapper mapper,
            IProjectRepository projectRepository,
            IContextTaskRepository contextTaskRepository,
            IHistoricalTaskRepository historicalTaskRepository
            )
        {
            _mapper = mapper;
            _contextTaskRepository = contextTaskRepository;
            _projectRepository = projectRepository;
            _historicalTaskRepository = historicalTaskRepository;
        }

        public async Task<ContextTaskDTO?> CreateContextTaskAsync(ContextTaskDTO contextTaskDTO)
        {
            ContextTask? contextTaskCreated = null;
            var contextTask = _mapper.Map<ContextTask>(contextTaskDTO);
            contextTask.UpdateAt = DateTime.Now;

            var project = await _projectRepository.GetAsync(contextTask.ProjectId);
            if (project != null)
            {
                contextTaskCreated = await _contextTaskRepository.CreateAsync(contextTask);
            }

            return contextTaskCreated != null
                ? _mapper.Map<ContextTaskDTO>(contextTaskCreated)
                : null;
        }

        public async Task<bool> DisableContextTaskByIdAsync(int contextTaskId)
        {
            return await _contextTaskRepository.DisableAsync(contextTaskId);
        }

        public async Task<ProjectDTO?> GetContextTaskAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            if (project != null)
            {
                project.ContextTasks = await _contextTaskRepository.GetAllAsync(projectId);
            }

            return project != null
                ? _mapper.Map<ProjectDTO>(project)
                : null;
        }

        public async Task<ContextTaskDTO?> UpdateContextTaskAsync(ContextTaskDTO contextTaskDTO)
        {
            ContextTask? contextTaskUpdated = null;
            
            var contextTask = _mapper.Map<ContextTask>(contextTaskDTO);
            var currentContextTask = await _contextTaskRepository.GetAsync(contextTask.Id);
            if (currentContextTask != null)
            {
                contextTask.UpdateAt = currentContextTask.UpdateAt;
                contextTask.ProjectId = currentContextTask.ProjectId;

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
                ? _mapper.Map<ContextTaskDTO>(contextTaskUpdated)
                : null;
        }
    }
}
