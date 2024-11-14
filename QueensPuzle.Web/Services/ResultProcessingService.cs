using QueensPuzle.Web.Data;

namespace QueensPuzle.Web.Services
{
    public class ResultProcessingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ResultProcessingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                using (var scope = _serviceProvider.CreateScope()) 
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ResultContext>();

                }
            }
        }
    }
}
