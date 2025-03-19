using Npgsql;

namespace backend.Repositories
{
    public class DatabaseRepository
    {
        private NpgsqlConnection? _connection;
        public DatabaseRepository()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Database = "prodops",
                Username = "postgres",
                Password = "p"
            };

            string _connectionString = builder.ConnectionString;
            if (_connection == null)
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
            }
            else
            {
                System.Console.WriteLine("Database connection already exists.");
            }
        }


        public NpgsqlCommand CallDatabase(string query)
        {
            return new NpgsqlCommand(query, _connection);
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}
