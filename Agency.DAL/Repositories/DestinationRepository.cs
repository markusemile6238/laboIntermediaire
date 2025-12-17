using Agency.DAL.Database;
using Agency.Domaine.Entities;
using Agency.Domaine.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Agency.DAL.Mapper;
using Agency.DAL.ExceptionDAL;

namespace Agency.DAL.Repositories
{
    public class DestinationRepository : IDestinationRepo
    {

        private readonly IDbConnectionFactory _connection;
        private readonly IDbCommandFactory _command;
        private readonly ILogger<DestinationRepository> _logger;

        public DestinationRepository(IDbConnectionFactory connection, IDbCommandFactory command, ILogger<DestinationRepository> logger)
        {
            _connection = connection;
            _command = command;
            _logger = logger;
        }


        #region GETASYNC
        public async Task<IEnumerable<Destination>> GetAsync()
        {
            List<Destination> destinations = new List<Destination>();


            string sql = @"SELECT d.Id,d.Country,d.City,d.Description,d.IsEnable,d.CreatedAt,d.UpdatedAt,COUNT(a.Id) AS ActivityCount FROM Destinations d LEFT JOIN Activities a ON d.Id = a.DestinationId WHERE d.IsEnable = 1 GROUP BY d.Id,d.Country,d.City,d.Description,d.IsEnable,d.CreatedAt,d.UpdatedAt ORDER BY d.Country,d.City";
            try
            {
                using var connection = _connection.CreateConnection();
                using var command = _command.CreateCommand(sql, connection);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    destinations.Add(DestinationMapper.ToDestination(reader));
                }
                return destinations;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erreur SQL lors de la récupération des destinations");
                throw new Exception($"Erreur de base de données: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la récupérations des destination");
                throw;
            }
        }
        #endregion

        #region GETBYIDASYNC
        public async Task<Destination>? GetByIdAsync(int id)
        {
            if (id < 0) { throw new ArgumentOutOfRangeException(nameof(id)); }


            try
            {

                string sql = "SELECT d.Id,d.Country,d.City,d.Description,d.IsEnable,d.CreatedAt,d.UpdatedAt,COUNT(a.Id) AS ActivityCount FROM Destinations d LEFT JOIN Activities a ON d.Id = a.DestinationId WHERE d.Id = @Id AND d.IsEnable = 1 GROUP BY d.Id,d.Country,d.City,d.Description,d.IsEnable,d.CreatedAt,d.UpdatedAt ORDER BY d.Country,d.City";

                using var connection = _connection.CreateConnection();

                // add parameters first query
                var parameters = new[]
                {
                new SqlParameter("@Id", id)
                };
                
                using var command = _command.CreateParameterizedCommand(sql, connection, parameters);

                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                   return   DestinationMapper.ToDestination(reader);                 
                }
                else
                {
                    _logger.LogWarning("Destination inconnue ");
                    throw new KeyNotFoundException($"Destination id:{id} inconnu !");
                }
            
                // deuxieme requete


            }
            catch (SqlException ex) when (ex.Number == 207)
            {
                _logger.LogError(ex, "Colonne invalide dans la requête pour ID {Id}", id);
                throw new RepositoryException($"Erreur de structure de base de données", ex);
            }

        }

        #endregion

        #region FINDBYKEYWORD
        public Task<Destination>? FindByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CREATEASYNC
        public async Task<Destination> CreateAsync(Destination destination)
        {
            string sql = @"INSERT INTO Destinations (Country,City,Description) OUTPUT INSERTED.* VALUES(@Country,@City,@Description)";

            var parameters = new[]
            {
                new SqlParameter("@Country",destination.Country),
                new SqlParameter("@City",destination.City),
                new SqlParameter("@description",destination.Description)
            };
            try
            {

                using var connection = _connection.CreateConnection();
                using var command = _command.CreateParameterizedCommand(sql, connection, parameters);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return DestinationMapper.ToDestination(reader);
                }
                throw new InvalidOperationException("Aucune destination créer");

            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                _logger.LogWarning(ex, "Duplication {country}{city}", destination.Country, destination.City);

                throw new DuplicateEntryException(entity: "Destination", field: "City", value: destination.City, innerException: ex);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erreur SQL lors de la création d'une destination");
                throw new Exception($"Erreur de base de données: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la création d'une destination");
                throw;
            }
        }
        #endregion

        #region UPDATEASYNCH
        public Task<Destination> UpdateAsync(Destination destination)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DELETEASYNC
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion






    }
}
