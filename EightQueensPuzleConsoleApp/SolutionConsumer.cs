using System.Collections.Concurrent;

namespace EightQueensPuzleConsoleApp
{
    public class SolutionConsumer : ISolutionConsumer
    {
        private readonly SolutionNormalizer _normalizer;

        public SolutionConsumer(SolutionNormalizer normalizer)
        {
            _normalizer = normalizer;
        }

        public int CountFundamentalSolutions(BlockingCollection<int[]> solutions)
        {
            var uniqueSolutions = new ConcurrentDictionary<string, bool>();
            int processedCount = 0;

            Parallel.ForEach(solutions.GetConsumingEnumerable(), solution =>
            {
                string normalized = _normalizer.Normalize(solution);
                uniqueSolutions.TryAdd(normalized, true);

                Interlocked.Increment(ref processedCount);
                if (processedCount % 100 == 0)
                {
                    //Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Solutions processed: {processedCount}");
                }
            });

            return uniqueSolutions.Count;
        }
    }
}
