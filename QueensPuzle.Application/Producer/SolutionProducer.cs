using System.Collections.Concurrent;

namespace QueensPuzle.Application.Producer
{
    public class SolutionProducer : ISolutionProducer
    {
        public void ProduceSolutions(int n, BlockingCollection<int[]> solutions, ConcurrentBag<int> solutionCounter)
        {
            if (n == 0)
            {
                solutionCounter.Add(1);
                return;
            }
            SolveNQueensRecursive(n, 0, new int[n], solutions, solutionCounter);
        }

        private void SolveNQueensRecursive(int n, int row, int[] queens, BlockingCollection<int[]> solutions, ConcurrentBag<int> solutionCounter)
        {
            if (row == n)
            {
                solutions.Add((int[])queens.Clone());
                solutionCounter.Add(1);
                if (solutionCounter.Count % 100 == 0)
                {
                    //Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Solutions generated: {solutionCounter.Count}");
                }
                return;
            }

            for (int col = 0; col < n; col++)
            {
                if (IsSafe(queens, row, col))
                {
                    queens[row] = col;
                    SolveNQueensRecursive(n, row + 1, queens, solutions, solutionCounter);
                }
            }
        }

        private bool IsSafe(int[] queens, int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                int otherCol = queens[i];
                if (otherCol == col || Math.Abs(otherCol - col) == Math.Abs(i - row))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
