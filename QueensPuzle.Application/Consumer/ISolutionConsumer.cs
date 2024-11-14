using System.Collections.Concurrent;

namespace QueensPuzle.Application.Consumer
{
    public interface ISolutionConsumer
    {
        int CountFundamentalSolutions(BlockingCollection<int[]> solutions);
    }
}
