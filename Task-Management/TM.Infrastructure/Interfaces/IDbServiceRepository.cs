namespace TM.Infrastructure.Repositories
{
    public interface IDbServiceRepository
    {
        Task<int> ExecuteAsync(string query, object parameters);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters);
        Task<T> QueryFirstAsync<T>(string query, object parameters);
    }
}