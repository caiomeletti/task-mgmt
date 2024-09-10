using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TM.Infrastructure.Utilities;

namespace TM.Infrastructure.Repositories
{
    public class DbHelper : IDbHelper
    {
        private readonly IConfiguration _configuration;

        public DbHelper(IConfiguration configuration) => _configuration = configuration;

        public async Task<T> QueryFirstAsync<T>(string query, object parameters)
        {
            await using var connection = GetSqlConnection();
            var ret = await connection.QueryFirstAsync<T>(query, parameters).ConfigureAwait(false);
            return ret;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters)
        {
            await using var connection = GetSqlConnection();
            var ret = await connection.QueryAsync<T>(query, parameters).ConfigureAwait(false);
            return ret;
        }

        public async Task<int> ExecuteAsync(string query, object parameters)
        {
            await using var connection = GetSqlConnection();
            return await connection.ExecuteAsync(query, parameters).ConfigureAwait(false);
        }

        private MySqlConnection GetSqlConnection() => new(_configuration.GetConnectionString(DBConstants.ConnectionStringName));
    }
}
