using Agency.DAL.Database;
using Agency.Domaine.Repositories;

namespace Agency.API.Middleware
{
    public class DiTestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DiTestMiddleware> _logger;

        public DiTestMiddleware(RequestDelegate next, ILogger<DiTestMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            _logger.LogInformation("Test du conteneur DI...");

            try
            {
                // Test 1: DbConnectionFactory
                var connectionFactory = serviceProvider.GetService<IDbConnectionFactory>();
                _logger.LogInformation("DbConnectionFactory: {Status}",
                    connectionFactory != null ? "OK" : "NULL");

                // Test 2: DestinationRepository
                var destinationRepo = serviceProvider.GetService<IDestinationRepo>();
                _logger.LogInformation("DestinationRepository: {Status}",
                    destinationRepo != null ? "OK" : "NULL");

                // Test 3: Configuration
                var configuration = serviceProvider.GetService<IConfiguration>();
                var connString = configuration?.GetConnectionString("main");
                _logger.LogInformation("Connection string: {HasValue}",
                    !string.IsNullOrEmpty(connString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du test DI");
            }

            await _next(context);
        }
    }
}
