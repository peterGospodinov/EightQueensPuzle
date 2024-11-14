using System.Collections.Concurrent;

namespace QueensPuzzle.Application.Consumer
{
    public interface ISolutionConsumer
    {
        int CountFundamentalSolutions(BlockingCollection<int[]> solutions);
    }
}
