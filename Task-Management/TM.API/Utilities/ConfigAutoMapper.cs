using AutoMapper;
using TM.API.ViewModels.ContextTask;
using TM.API.ViewModels.Project;
using TM.API.ViewModels.TaskComment;
using TM.Domain.Entities;
using TM.Services.DTO;

namespace TM.API.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigAutoMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectDTO>().ReverseMap();
                cfg.CreateMap<CreateProjectViewModel, ProjectDTO>();
                cfg.CreateMap<ContextTask, ContextTaskDTO>().ReverseMap();
                cfg.CreateMap<CreateContextTaskViewModel, ContextTaskDTO>();
                cfg.CreateMap<ContextTask, HistoricalTask>().ReverseMap();
                cfg.CreateMap<CreateTaskCommentViewModel, TaskCommentDTO>();
                cfg.CreateMap<TaskComment, TaskCommentDTO>().ReverseMap();
                cfg.CreateMap<ContextTaskAggregate, ContextTaskAggregateDTO>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}
