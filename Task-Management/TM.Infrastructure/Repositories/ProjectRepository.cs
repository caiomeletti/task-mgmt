using Dapper;
using System.Text;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbServiceRepository _dbService;
        private string _baseSelect;

        public ProjectRepository(
            IDbServiceRepository dbService)
        {
            _dbService = dbService;
            _baseSelect =
                @"SELECT " +
                "    p.Id " +
                "   ,p.Title " +
                "   ,p.Description " +
                "   ,p.UpdateAt " +
                "   ,p.UserId " +
                "FROM project p " +
                "WHERE p.Enabled = 1 ";
        }

        public async Task<Project> CreateAsync(Project project)
        {
            string sql =
                "INSERT INTO project " +
                "   (Title, Description, UpdateAt, UserId, Enabled) " +
                //"OUTPUT LAST_INSERT_ID() " +
                "VALUES " +
                "   (@Title, @Description, @UpdateAt, @UserId, 1);";

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

        public async Task<bool> DisableAsync(int projectId)
        {
            string sql = "UPDATE project SET Enabled = 0 WHERE Id = @Id AND Enabled = 1;";

            var param = new DynamicParameters();
            param.Add("Id", projectId);

            var rowsAffected = await _dbService.ExecuteAsync(sql, param);

            return rowsAffected > 0;
        }

        public async Task<Project?> GetAsync(int projectId)
        {
            Project? ret = null;
            try
            {
                var sql = new StringBuilder(_baseSelect);
                sql.Append("AND p.Id = @Id ");

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

        public async Task<IEnumerable<Project>?> GetAsync(int projectId, int userId)
        {
            IEnumerable<Project>? ret = null;
            try
            {
                var sql = new StringBuilder(_baseSelect);

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
