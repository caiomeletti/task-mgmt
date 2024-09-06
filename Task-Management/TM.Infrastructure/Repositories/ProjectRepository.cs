using Mysqlx.Crud;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbServiceRepository _dbService;

        public ProjectRepository(
            IDbServiceRepository dbService)
        {
            _dbService = dbService;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            string sql = 
                "INSERT INTO project " + 
                "(Title, Description, UpdateAt) " +
                //"OUTPUT LAST_INSERT_ID() " +
                "VALUES " + 
                "(@Title, @Description, @UpdateAt);";
            object[] parameters = 
            {
                new
                {
                    project.Title,
                    project.Description,
                    project.UpdateAt
                }
            };
            var lastInsertId = await _dbService.ExecuteAsync(sql, parameters);
            //TODO retorno do ultimo id inserido
            project.Id = lastInsertId;

            return project;
        }

        public async Task<IEnumerable<Project>?> GetProjectAsync()
        {
            IEnumerable<Project>? ret = null;
            try
            {
                var sql =
                    @"SELECT " +
                    "    p.Id " +
                    "   ,p.Title " +
                    "   ,p.Description " +
                    "   ,p.UpdateAt " +
                    "FROM project p ";

                var param = new { };

                ret = await _dbService.QueryAsync<Project>(sql, param);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return ret;
        }
    }
}
