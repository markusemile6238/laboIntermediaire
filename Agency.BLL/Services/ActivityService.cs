using Agency.DAL.ExceptionDAL;
using Agency.Domaine.Entities;
using Agency.Domaine.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.BLL.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepo _repo;
        private readonly IDestinationRepo _destinationRepo;
        private readonly ILogger<ActivityService> _logger;

        public ActivityService(IActivityRepo repo, IDestinationRepo destinationRepo, ILogger<ActivityService> logger)
        {
            _repo = repo;
            _destinationRepo = destinationRepo;
            _logger = logger;
        }

        #region CREATEASYNC
        public async Task<Activity> CreateAsync(Activity activity)
        {
            try
            {
                // validation metier
                if (string.IsNullOrWhiteSpace(activity.Name))
                    throw new ArgumentException("Le Nom est requis");

                if (string.IsNullOrWhiteSpace(activity.Description))
                    throw new ArgumentException("Le Nom est requis");

                if (activity.Price <= 0)
                    throw new ArgumentException("Le prix doit être plus gtand que 0");

                Task<Destination>? destinationExist = _destinationRepo.GetByIdAsync(activity.DestinationId);

                if (destinationExist == null)
                    throw new ArgumentException("la destination n'esixte pas");

                return await _repo.CreateAsync(activity);
            }
            catch (DuplicateEntryException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création d'une activitée");
                throw new Exception("Erreur lors de la création d'une activitée", ex);
            }
        } 
        #endregion

        public Task<int> DeleteAsynch(int id)
        {
            throw new NotImplementedException();
        }

        #region GETASYNC
        public async Task<IEnumerable<Activity>> GetAsync()
        {
            try
            {
                var activities = await _repo.GetAsync();
                if (!activities.Any())
                {
                    _logger.LogInformation("Aucune destination trouvée");
                    return Enumerable.Empty<Activity>();
                }

                return activities;
            }
            catch (SqlException ex)
            {
                throw new Exception($"SQL Exception : {ex.Message} : {ex.Number}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Servci!e Activity {ex.Message}");
            }
        } 
        #endregion

        public Task<Activity>? GetByIdAsyn(int id)
        {
            throw new NotImplementedException();
        }
    }
}
