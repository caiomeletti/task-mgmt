using Dapper;
using System.Text;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class ContextTaskRepository : BaseRepository, IContextTaskRepository
    {
        private string _baseSelect;

        public ContextTaskRepository(
            IDbServiceRepository dbService) : base(dbService)
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

        public async Task<IEnumerable<ContextTask>?> GetAsync(int projectId)
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

    }
}
