using System;
using Npgsql;

namespace Amethyst.Demo.Querying.Tests.Infrastructure
{
    public class TempDatabase : IDisposable
    {
        public string ConnectionString { get; }

        private readonly NpgsqlConnectionStringBuilder _connectionStringBuilder;
        private readonly string _newDbName;

        public TempDatabase(NpgsqlConnectionStringBuilder connectionStringBuilder, string newDbName)
        {
            if (string.IsNullOrWhiteSpace(newDbName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(newDbName));

            _connectionStringBuilder = connectionStringBuilder;
            _newDbName = newDbName;

            var newDbConnectionString = new NpgsqlConnectionStringBuilder(_connectionStringBuilder.ConnectionString)
            {
                Database = _newDbName
            };

            ConnectionString = newDbConnectionString.ConnectionString;
        }

        public void Dispose()
        {
            using var connection = new NpgsqlConnection(_connectionStringBuilder.ConnectionString);
            using var command = new NpgsqlCommand($@"
                REVOKE CONNECT ON DATABASE {_newDbName} FROM public;
                SELECT pid, pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{_newDbName}';
                DROP DATABASE {_newDbName};", connection);
            
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}