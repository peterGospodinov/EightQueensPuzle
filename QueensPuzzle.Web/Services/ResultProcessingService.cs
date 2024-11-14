using Microsoft.EntityFrameworkCore;
using QueensPuzle.Web.Data;
using QueensPuzzle.Application.Consumer;
using QueensPuzzle.Application.Models;
using QueensPuzzle.Application.Normalizer;
using QueensPuzzle.Application.Producer;
using QueensPuzzle.Application.Services;

namespace QueensPuzle.Web.Services
{
    public class ResultProcessingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private int _n = 1;
        public ResultProcessingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _n = GetLastInsertedIdAsync().Result +1;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {        
            while (!stoppingToken.IsCancellationRequested) 
            {
                using (var scope = _serviceProvider.CreateScope()) 
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ResultContext>();
                    
                    ISolutionProducer producer = new SolutionProducer();
                    SolutionNormalizer normalizer = new SolutionNormalizer();
                    ISolutionConsumer consumer = new SolutionConsumer(normalizer);

                    var service = new NQueensService(producer, consumer);
                    SolutionResult solutionResult = await service.SolvePuzle(_n);

                    bool success = await InsertSolutionResultAsync(dbContext, solutionResult);
                    if (success)
                    {
                        _n++;
                    }
                }
                await Task.Delay(TimeSpan.FromMilliseconds(100), stoppingToken);
            }
        }

        private async Task<int> GetLastInsertedIdAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ResultContext>();
                var lastResult = await dbContext.SolutionResults
                                        .OrderByDescending(r => r.Id)
                                        .FirstOrDefaultAsync();
                return lastResult?.Id ?? 0;
            }
        }

        private async Task<bool> InsertSolutionResultAsync(ResultContext dbContext, SolutionResult solutionResult)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SolutionResults ON");
                    dbContext.SolutionResults.Add(solutionResult);
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SolutionResults OFF");
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
        private async Task<int> GetLastInsertedIdAsync(ResultContext dbContext)
        {
            var lastResult = await dbContext.SolutionResults
                                            .OrderByDescending(r => r.Id)
                                            .FirstOrDefaultAsync();
            return lastResult?.Id ?? 0;  
        }
    }
}
