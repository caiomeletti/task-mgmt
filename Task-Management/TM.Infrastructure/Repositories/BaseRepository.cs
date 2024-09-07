namespace TM.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly IDbServiceRepository _dbService;

        protected BaseRepository(
            IDbServiceRepository dbService)
        {
            _dbService = dbService;
        }

        protected async Task<int> GetLastInsertedIdAsync()
        {
            return await _dbService.QueryFirstAsync<int>("SELECT LAST_INSERT_ID();", new { });
        }
    }
}