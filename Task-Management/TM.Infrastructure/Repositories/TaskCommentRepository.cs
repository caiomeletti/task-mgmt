using Dapper;
using System.Text;
using TM.Domain.Entities;
using TM.Infrastructure.Interfaces;

namespace TM.Infrastructure.Repositories
{
    public class TaskCommentRepository : BaseRepository, ITaskCommentRepository
    {
        private readonly string _baseSelect;

        public TaskCommentRepository(
            IDbServiceRepository dbService) : base(dbService)
        {
            _baseSelect =
                @"SELECT " +
                "	 t.Id " +
                "	,t.ContextTaskId " +
                "	,t.Comment " +
                "	,t.UserId " +
                "	,t.UpdateAt " +
                "	,t.Enabled " +
                "FROM task_comment t " +
                "WHERE t.Enabled = 1 ";
        }

        public async Task<IEnumerable<TaskComment>?> GetAllAsync(int contextTaskId)
        {
            IEnumerable<TaskComment>? ret = null;
            try
            {
                var sql = new StringBuilder(_baseSelect);

                var param = new DynamicParameters();
                if (contextTaskId != -1)
                {
                    sql.Append("AND t.ContextTaskId = @ContextTaskId");
                    param.Add("ContextTaskId", contextTaskId);
                }

                ret = await _dbService.QueryAsync<TaskComment>(sql.ToString(), param);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return ret;
        }

        public async Task<TaskComment?> GetAsync(int taskCommentId)
        {
            TaskComment? ret = null;
            try
            {
                var sql = new StringBuilder(_baseSelect);

                var param = new DynamicParameters();
                if (taskCommentId != -1)
                {
                    sql.Append("AND t.Id = @Id");
                    param.Add("Id", taskCommentId);
                }

                ret = await _dbService.QueryFirstAsync<TaskComment>(sql.ToString(), param);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return ret;
        }
		
        public async Task<TaskComment> CreateAsync(TaskComment taskComment)
        {
            string sql =
                @"INSERT INTO task_comment " +
                "   (ContextTaskId " +
                "   ,Comment " +
                "   ,UserId " +
                "   ,UpdateAt " +
                "   ,Enabled) " +
                "VALUES " +
                "   (@ContextTaskId " +
                "   ,@Comment " +
                "   ,@UserId " +
                "   ,@UpdateAt " +
                "   ,1); ";

            var param = new
            {
                taskComment.ContextTaskId,
                taskComment.Comment,
                taskComment.UserId,
                taskComment.UpdateAt,
            };
            var affectedRows = await _dbService.ExecuteAsync(sql, param);
            if (affectedRows > 0)
            {
                var lastInsertId = await GetLastInsertedIdAsync();
                taskComment.Id = lastInsertId;
            }

            return taskComment;
        }

        public async Task<TaskComment> UpdateAsync(TaskComment taskComment)
        {
            string sql =
               @"UPDATE task_comment SET " +
               "    Comment = @Comment " +
               "   ,UserId = @UserId " +
               "   ,UpdateAt = @UpdateAt " +
               "WHERE " +
               "    Id = @Id ";

            var param = new
            {
                taskComment.Comment,
                taskComment.UserId,
                taskComment.UpdateAt,
                taskComment.Id
            };
            var affectedRows = await _dbService.ExecuteAsync(sql, param);
            if (affectedRows > 0)
            {
            }

            return taskComment;
        }
    }
}
