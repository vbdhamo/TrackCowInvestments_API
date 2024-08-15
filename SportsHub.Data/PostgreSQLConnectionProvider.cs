using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql;

namespace SportsHub.Data
{
    public class PostgreSQLConnectionProvider
    {
        private readonly ConnectionStrings _connectionString;
        public PostgreSQLConnectionProvider(IOptions<ConnectionStrings> config)
        {
            _connectionString = config.Value;
        }

        public NpgsqlConnection GetPostgreSQLConnection()
        {
            var connection = GetPostgreSQLConnectionFromConfiguration();
            connection.Open();

            return connection;
        }


        private NpgsqlConnection GetPostgreSQLConnectionFromConfiguration()
        {
            var connection = new NpgsqlConnection()
            {
                ConnectionString = _connectionString.DefaultConnection // GetConnectionString()
            };

            return connection;
        }

        public async Task<NpgsqlConnection> GetPostgreSQLConnectionAsync()
        {
            var connection = GetPostgreSQLConnectionFromConfiguration();
            await connection.OpenAsync();

            return connection;
        }
    }
}
