using EightQueensPuzleConsoleApp.Models;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace EightQueensPuzleConsoleApp
{
    public class NQueensService
    {
        private readonly ISolutionProducer _producer;
        private readonly ISolutionConsumer _consumer;

        public NQueensService(ISolutionProducer producer, ISolutionConsumer consumer)
        {
            _producer = producer;
            _consumer = consumer;
        }

        public async Task<SolutionResult> SolvePuzle(int n)
        {
            SolutionResult result = new SolutionResult();

            var solutions = new BlockingCollection<int[]>();
            var solutionCounter = new ConcurrentBag<int>();
            int fundamentalSolutions = 0;

            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Starting {n}-Queens Problem");

            var stopwatch = Stopwatch.StartNew();

            var producerTask = Task.Run(() => _producer.ProduceSolutions(n, solutions, solutionCounter));
            var consumerTask = Task.Run(() =>
            {
                fundamentalSolutions = _consumer.CountFundamentalSolutions(solutions);
                stopwatch.Stop();
            });

            await producerTask;
            solutions.CompleteAdding();
            await consumerTask;

            result.N = n;
            result.FundamentalSolutionsCount = fundamentalSolutions;
            result.TotalSolutionsCount = solutionCounter.Count;
            result.ElapsedTime = stopwatch.Elapsed.ToString();
            return result;
        }
    }
}
