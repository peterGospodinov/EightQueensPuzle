using System.Collections.Concurrent;

namespace EightQueensPuzleConsoleApp
{
    public interface ISolutionConsumer
    {
        int CountFundamentalSolutions(BlockingCollection<int[]> solutions);
    }
}
