using System.Collections.Concurrent;

namespace QueensPuzle.Application.Producer
{
    public interface ISolutionProducer
    {
        void ProduceSolutions(int n, BlockingCollection<int[]> solutions, ConcurrentBag<int> solutionCounter);
    }
}
