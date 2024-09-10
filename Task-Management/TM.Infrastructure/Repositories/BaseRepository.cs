namespace TM.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly IDbHelper _dbService;

        protected BaseRepository(
            IDbHelper dbService)
        {
            _dbService = dbService;
        }

        protected async Task<int> GetLastInsertedIdAsync()
        {
            return await _dbService.QueryFirstAsync<int>("SELECT LAST_INSERT_ID();", new { });
        }
    }
}