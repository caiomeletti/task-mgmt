using Dapper;
using System.Text;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class ContextTaskRepository : IContextTaskRepository
    {
        private readonly IDbServiceRepository _dbService;
        private string _baseSelect;

        public ContextTaskRepository(
            IDbServiceRepository dbService)
        {
            _dbService = dbService;
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
    }
}
