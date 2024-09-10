using Dapper;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class HistoricalTaskRepository : BaseRepository, IHistoricalTaskRepository
    {
        public HistoricalTaskRepository(
           IDbHelper dbService) : base(dbService)
        {
        }

        public async Task<HistoricalTask> CreateAsync(HistoricalTask historicalTask)
        {
            string sql =
                @"INSERT INTO historical_task " +
                "   (Title " +
                "   ,Description " +
                "   ,DueDate " +
                "   ,Priority " +
                "   ,Status " +
                "   ,ContextTaskId " +
                "   ,UpdateAt " +
                "   ,UserId) " +
                "VALUES " +
                "   (@Title " +
                "   ,@Description " +
                "   ,@DueDate " +
                "   ,@Priority " +
                "   ,@Status " +
                "   ,@ContextTaskId " +
                "   ,@UpdateAt " +
                "   ,@UserId); ";

            var param = new DynamicParameters();
            param.Add("Title", historicalTask.Title);
            param.Add("Description", historicalTask.Description);
            param.Add("DueDate", historicalTask.DueDate);
            param.Add("Priority", historicalTask.Priority);
            param.Add("Status", historicalTask.Status);
            param.Add("ContextTaskId", historicalTask.ContextTaskId);
            param.Add("UpdateAt", historicalTask.UpdateAt);
            param.Add("UserId", historicalTask.UserId);

            var affectedRows = await _dbService.ExecuteAsync(sql, param);
            if (affectedRows > 0)
            {
                var lastInsertId = await GetLastInsertedIdAsync();
                historicalTask.Id = lastInsertId;
            }

            return historicalTask;
        }
    }
}
