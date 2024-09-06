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
