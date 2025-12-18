using Agency.DAL.ExceptionDAL;
using Agency.Domaine.Entities;
using Agency.Domaine.Repositories;
using Microsoft.Extensions.Logging;

namespace Agency.BLL.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepo _repo;
        private readonly ILogger<DestinationService> _logger;

        public DestinationService(IDestinationRepo repo, ILogger<DestinationService> logger)
        {
            _repo = repo;
            _logger = logger;
        }


        #region CREATEASYNC
        public async Task<Destination> CreateAsync(Destination destination)
        {
            try
            {

            // Validation métier (optionnel)
            if (string.IsNullOrWhiteSpace(destination.Country))
                throw new ArgumentException("Le pays est requis");

            if (string.IsNullOrWhiteSpace(destination.City))
                throw new ArgumentException("La ville est requise");


            return await _repo.CreateAsync(destination);
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
                _logger.LogError(ex, "Erreur lors de la création d'une destination");
                throw new Exception("Erreur lors de la création de la destination", ex);
            }
        }

       
        #endregion

        #region GETASYNC
        public async Task<IEnumerable<Destination>> GetAsync()
        {
            try
            {
                var destinations = await _repo.GetAsync();

                if (!destinations.Any())
                {
                    _logger.LogInformation("Aucune destination trouvée");
                    return Enumerable.Empty<Destination>();
                }
                return destinations;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #region GetByIdAsync
        public Task<Destination>? GetByIdAsyn(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException("id");

            try
            {
                return _repo.GetByIdAsync(id);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region DELETE ASYNC
        public Task<int> DeleteAsynch(int id)
        {
            if (id < 0) throw new InvalidOperationException(nameof(id));

            return _repo.DeleteAsync(id);

        } 
        #endregion

    }
}
