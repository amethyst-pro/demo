using System;
using System.Threading;
using Npgsql;

namespace Amethyst.Demo.Querying.Tests.Infrastructure
{
    public sealed class PostgresConfiguration
    {
        private const string PostgreServerVariableName = "POSTGRE_TEST_SERVER";
        private const string PostgreServerPortVariableName = "POSTGRE_TEST_SERVER_PORT";

        private static int _number = 1;
        private readonly NpgsqlConnectionStringBuilder _connectionStringBuilder;
        
        public PostgresConfiguration()
        {
            _connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Pooling = false,
                Host = Environment.GetEnvironmentVariable(PostgreServerVariableName)
                       ?? DefaultPostgreSettings.Host,
                Username = "postgres",
                Password = "postgres",
                Database = "postgres",
                Port = Environment.GetEnvironmentVariable(PostgreServerPortVariableName) != null
                    ? int.Parse(Environment.GetEnvironmentVariable(PostgreServerPortVariableName) ?? throw new InvalidOperationException())
                    : DefaultPostgreSettings.Port
            };
        }
        
        public TempDatabase CreateDatabase(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = "test_es" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + Interlocked.Increment(ref _number);

            using (var connection = new NpgsqlConnection(_connectionStringBuilder.ConnectionString))
            {
                using var command = new NpgsqlCommand($"CREATE DATABASE {name};", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            return new TempDatabase(_connectionStringBuilder, name);
        }
    }
}