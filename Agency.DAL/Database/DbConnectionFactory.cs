using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Database
{
    public class DbConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection? _currentConnection;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("main") ??
                throw new InvalidOperationException("Pas de chaine de connection");
        }

        public SqlConnection CreateConnection()
        {
            try
            {

            var connection = new SqlConnection(_connectionString);
            connection.Open();
            _currentConnection = connection;
            return connection;
            }
            catch(SqlException ex)
            {
                throw new InvalidOperationException($"Echec de connection :{ex.Message}");
            }
        }

        public async Task<SqlConnection> CreateConnectionAsync()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                _currentConnection = connection; // Pour tracking si nécessaire
                return connection;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Échec de création de la connexion: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _currentConnection?.Close();
            _currentConnection?.Dispose();
        }
    }
}
