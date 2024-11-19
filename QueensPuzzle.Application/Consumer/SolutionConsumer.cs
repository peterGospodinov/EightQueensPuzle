using System.Collections.Concurrent;
using QueensPuzzle.Application.Normalizer;

namespace QueensPuzzle.Application.Consumer
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
           
            Parallel.ForEach(solutions.GetConsumingEnumerable(), solution =>
            {
                string normalized = _normalizer.Normalize(solution);
                uniqueSolutions.TryAdd(normalized, true);
            });

            return uniqueSolutions.Count;
        }
    }
}
