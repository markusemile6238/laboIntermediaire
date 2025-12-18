using Agency.DAL.Database;
using Agency.DAL.ExceptionDAL;
using Agency.DAL.Mapper;
using Agency.Domaine.Entities;
using Agency.Domaine.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
namespace Agency.DAL.Repositories
{
    public class ActivityRepository : IActivityRepo
    {


        private readonly IDbConnectionFactory _connection;
        private readonly IDbCommandFactory _command;
        private readonly ILogger<ActivityRepository> _logger;

        public ActivityRepository(IDbConnectionFactory connection, IDbCommandFactory command, ILogger<ActivityRepository> logger)
        {
            _connection = connection;
            _command = command;
            _logger = logger;
        }



        #region CREATEASYNC
        public async Task<Activity> CreateAsync(Activity activity)
        {
            if (activity is null)
                throw new ArgumentNullException(nameof(activity));

            string sql = @"INSERT INTO Activities (Name,Description,Price,DestinationId) OUTPUT INSERTED.* VALUES (@Name,@Description,@Price,@DestinationId)";

            try
            {
                var parameters = new[]
                {
                new SqlParameter("@Name",activity.Name),
                new SqlParameter("@Description",activity.Description),
                new SqlParameter("@Price",activity.Price),
                new SqlParameter("@DestinationId",activity.DestinationId)
                    };

                using var connection = _connection.CreateConnection();
                using var command = _command.CreateParameterizedCommand(sql, connection, parameters);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return ActivityMapper.ToActivity(reader);
                }
                throw new InvalidOperationException("Aucune activitée créer");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                _logger.LogWarning(ex, "Duplication {name}", activity.Name);

                throw new DuplicateEntryException(entity: "Activité", field: "ce nom", value: activity.Name, innerException: ex);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erreur SQL lors de la création d'une activitée");
                throw new Exception($"Erreur de base de données: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la création d'une activitée");
                throw;
            }

        } 
        #endregion




        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        #region GETASYNC
        public async Task<IEnumerable<Activity>> GetAsync()
        {
            List<Activity> activities = new List<Activity>();

            string sql = @"SELECT a.Id,a.Name,a.Description,a.Price,a.DestinationId,a.CreatedAt,a.UpdatedAt,d.Country as CountryName 
                            FROM Activities a 
                            LEFT JOIN Destinations d ON a.DestinationId = d.Id AND d.IsEnable = 1
                            WHERE a.IsEnable = 1  
                            GROUP BY a.Id,a.Name,a.Description,a.Price,a.DestinationId,a.CreatedAt,a.UpdatedAt,d.Country 
                            ORDER BY d.Country";

            try
            {
                using var connection = _connection.CreateConnection();
                using var command = _command.CreateCommand(sql, connection);
                using var reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    activities.Add(ActivityMapper.ToActivity(reader));
                }
                return activities;


            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erreur SQL lors de la récupération des activités");
                throw new Exception($"Erreur de base de données: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la récupérations des activitées");
                throw;
            }



        }
        #endregion

        public Task<Activity>? GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> UpdateAsync(Activity destination)
        {
            throw new NotImplementedException();
        }
    }
}
