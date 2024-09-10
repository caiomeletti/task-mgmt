using Dapper;
using System.Text;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class ContextTaskRepository : BaseRepository, IContextTaskRepository
    {
        private readonly string _baseSelect;

        public ContextTaskRepository(
            IDbHelper dbService) : base(dbService)
        {
            _baseSelect =
                @"SELECT " +
                "	 t.Id " +
                "	,t.Title " +
                "	,t.Description " +
                "	,t.DueDate " +
                "	,t.Priority " +
                "	,t.Status " +
                "	,t.ProjectId " +
                "	,t.UpdateAt " +
                "	,t.UserId " +
                "	,t.Enabled " +
                "FROM context_task t " +
                "WHERE t.Enabled = 1 ";
        }

        public async Task<IEnumerable<ContextTask>?> GetAllAsync(int projectId)
        {
            IEnumerable<ContextTask>? ret = null;
            try
            {
                var sql = new StringBuilder(_baseSelect);

                var param = new DynamicParameters();
                if (projectId != -1)
                {
                    sql.Append("AND t.ProjectId = @ProjectId");
                    param.Add("ProjectId", projectId);
                }

                ret = await _dbService.QueryAsync<ContextTask>(sql.ToString(), param);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return ret;
        }

        public async Task<ContextTask?> GetAsync(int contextTaskId)
        {
            ContextTask? ret = null;
            try
            {
                var sql = new StringBuilder(_baseSelect);

                var param = new DynamicParameters();
                if (contextTaskId != -1)
                {
                    sql.Append("AND t.Id = @Id");
                    param.Add("Id", contextTaskId);
                }

                ret = await _dbService.QueryFirstAsync<ContextTask>(sql.ToString(), param);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return ret;
        }

        public async Task<ContextTask> CreateAsync(ContextTask contextTask)
        {
            string sql =
                @"INSERT INTO context_task " +
                "   (Title " +
                "   ,Description " +
                "   ,DueDate " +
                "   ,ProjectId " +
                "   ,UpdateAt " +
                "   ,UserId " +
                "   ,Enabled) " +
                "VALUES " +
                "   (@Title " +
                "   ,@Description " +
                "   ,@DueDate " +
                "   ,@ProjectId " +
                "   ,@UpdateAt " +
                "   ,@UserId " +
                "   ,1); ";

            var param = new
            {
                contextTask.Title,
                contextTask.Description,
                contextTask.DueDate,
                contextTask.ProjectId,
                contextTask.UpdateAt,
                contextTask.UserId
            };
            var affectedRows = await _dbService.ExecuteAsync(sql, param);
            if (affectedRows > 0)
            {
                var lastInsertId = await GetLastInsertedIdAsync();
                contextTask.Id = lastInsertId;
            }

            return contextTask;
        }

        public async Task<bool> DisableAsync(int contextTaskId)
        {
            string sql = "UPDATE context_task SET Enabled = 0 WHERE Id = @Id AND Enabled = 1;";

            var param = new DynamicParameters();
            param.Add("Id", contextTaskId);

            var rowsAffected = await _dbService.ExecuteAsync(sql, param);

            return rowsAffected > 0;
        }

        public async Task<ContextTask> UpdateAsync(ContextTask contextTask)
        {
            string sql =
               @"UPDATE context_task SET " +
               "    Title = @Title " +
               "   ,Description = @Description " +
               "   ,DueDate = @DueDate " +
               "   ,Priority = @Priority " +
               "   ,Status = @Status " +
               "   ,UpdateAt = @UpdateAt " +
               "   ,UserId = @UserId " +
               "WHERE " +
               "    Id = @Id ";

            var param = new
            {
                contextTask.Title,
                contextTask.Description,
                contextTask.DueDate,
                contextTask.Priority,
                contextTask.Status,
                contextTask.UpdateAt,
                contextTask.UserId,
                contextTask.Id
            };
            var affectedRows = await _dbService.ExecuteAsync(sql, param);
            if (affectedRows > 0)
            {
            }

            return contextTask;
        }
    }
}
