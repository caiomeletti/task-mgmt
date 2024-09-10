using Dapper;
using System.Text;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        private readonly string _baseSelect;

        public ProjectRepository(
            IDbHelper dbService) : base(dbService)
        {
            _baseSelect =
                @"SELECT " +
                "    p.Id " +
                "   ,p.Title " +
                "   ,p.Description " +
                "   ,p.UpdateAt " +
                "   ,p.UserId " +
                "   ,p.Enabled " +
                "FROM project p " +
                "WHERE p.Enabled = 1 ";
        }

        public async Task<Project> CreateAsync(Project project)
        {
            string sql =
                "INSERT INTO project " +
                "   (Title, Description, UpdateAt, UserId, Enabled) " +
                "VALUES " +
                "   (@Title, @Description, @UpdateAt, @UserId, 1);";

            var param = new
            {
                project.Title,
                project.Description,
                project.UpdateAt,
                project.UserId
            };
            var affectedRows = await _dbService.ExecuteAsync(sql, param);
            if (affectedRows > 0)
            {
                var lastInsertId = await GetLastInsertedIdAsync();
                project.Id = lastInsertId;
            }

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
