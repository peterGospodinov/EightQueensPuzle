using System.Collections.Concurrent;

namespace EightQueensPuzleConsoleApp
{
    public interface ISolutionProducer
    {
        void ProduceSolutions(int n, BlockingCollection<int[]> solutions, ConcurrentBag<int> solutionCounter);
    }
}
