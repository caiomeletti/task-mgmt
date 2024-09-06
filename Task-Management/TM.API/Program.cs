using AutoMapper;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TM.API.ViewModels;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1",
        Description = "",
        Contact = new OpenApiContact()
        {
            Name = "Caio Meletti",
            Email = "meletti@gmail.com",
            Url = new Uri("https://github.com/caiomeletti/")

        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

#region AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Project, ProjectDTO>().ReverseMap();
    cfg.CreateMap<CreateProjectViewModel, ProjectDTO>();
});
IMapper mapper = config.CreateMapper();
#endregion

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();