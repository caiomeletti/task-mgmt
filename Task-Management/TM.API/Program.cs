using AutoMapper;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;
using TM.Infrastructure.Repositories;
using TM.Services.DTO;
using TM.Services.Interfaces;
using TM.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDbServiceRepository, DbServiceRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddControllers();

#region AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Project, ProjectDTO>();
});
IMapper mapper = config.CreateMapper();
#endregion

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
