using Dapper;
using Mysqlx.Crud;
using System.Text;
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
                "   (Title, Description, UpdateAt) " +
                //"OUTPUT LAST_INSERT_ID() " +
                "VALUES " +
                "   (@Title, @Description, @UpdateAt, @UserId);";

            var param = new
            {
                project.Title,
                project.Description,
                project.UpdateAt,
                project.UserId
            };
            var lastInsertId = await _dbService.ExecuteAsync(sql, param);
            //TODO retorno do ultimo id inserido
            project.Id = lastInsertId;

            return project;
        }

        public async Task<Project?> GetProjectAsync(int projectId)
        {
            Project? ret = null;
            try
            {
                var sql = new StringBuilder(
                    @"SELECT " +
                    "    p.Id " +
                    "   ,p.Title " +
                    "   ,p.Description " +
                    "   ,p.UpdateAt " +
                    "   ,p.UserId " +
                    "FROM project p " +
                    "WHERE p.Id = @Id ");

                var param = new DynamicParameters();
                param.Add("Id", projectId);

                ret = await _dbService.QueryFirstAsync<Project>(sql.ToString(), param);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return ret;
        }

        public async Task<IEnumerable<Project>?> GetProjectAsync(int projectId, int userId)
        {
            IEnumerable<Project>? ret = null;
            try
            {
                var sql = new StringBuilder(
                    @"SELECT " +
                    "    p.Id " +
                    "   ,p.Title " +
                    "   ,p.Description " +
                    "   ,p.UpdateAt " +
                    "   ,p.UserId " +
                    "FROM project p " +
                    "WHERE 1 = 1 ");

                var param = new DynamicParameters();
                if (projectId != -1)
                {
                    sql.Append("AND p.Id = @Id");
                    param.Add("Id", projectId);
                }
                if (userId != -1)
                {
                    sql.Append("AND p.UserId = @UserId");
                    param.Add("UserId", userId);
                }

                ret = await _dbService.QueryAsync<Project>(sql.ToString(), param);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return ret;
        }
    }
}
