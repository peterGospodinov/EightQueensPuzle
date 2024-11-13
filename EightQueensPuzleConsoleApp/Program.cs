using EightQueensPuzleConsoleApp;
using System.Collections.Concurrent;

public class Program
{
    static async Task Main()
    {
        Console.WriteLine("Work in progress");

        SolutionNormalizer normalizer = new SolutionNormalizer();

        int n = 14;
        var solutions = new BlockingCollection<int[]>();
        var solutionCounter = new ConcurrentBag<int>();

        var producerTask = Task.Run(() => SolveNQueens(n, solutions, solutionCounter));
        var consumerTask = Task.Run(() =>
        {
            int fundamentalSolutions = CountFundamentalSolutions(solutions);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Total Fundamental Solutions for {n}-Queens: {fundamentalSolutions}\n");
        });

        await producerTask;
        solutions.CompleteAdding();
        await consumerTask;

        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Total Solutions for {n}-Queens: {solutionCounter.Count}\n");

        Console.ReadLine();
    }

    static void SolveNQueens(int n, BlockingCollection<int[]> solutions, ConcurrentBag<int> solutionCounter)
    {
        SolveNQueensRecursive(n, 0, new int[n], solutions, solutionCounter);
    }


    static void SolveNQueensRecursive(int n, int row, int[] queens, BlockingCollection<int[]> solutions, ConcurrentBag<int> solutionCounter)
    {
        if (row == n)
        {
            solutions.Add((int[])queens.Clone());
            solutionCounter.Add(1);
            if (solutionCounter.Count % 100 == 0)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Solutions generated: {solutionCounter.Count}".PadRight(Console.WindowWidth));
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

    static bool IsSafe(int[] queens, int row, int col)
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

    static int CountFundamentalSolutions(BlockingCollection<int[]> solutions)
    {
        var uniqueSolutions = new ConcurrentDictionary<string, bool>();
        int processedCount = 0;

        Parallel.ForEach(solutions.GetConsumingEnumerable(), solution =>
        {
            string normalized = NormalizeSolution(solution);
            uniqueSolutions.TryAdd(normalized, true);

            Interlocked.Increment(ref processedCount); 
            if (processedCount % 100 == 0)
            {
                //Console.SetCursorPosition(0, Console.CursorTop);
                //Console.Write($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Solutions processed: {processedCount}".PadRight(Console.WindowWidth));
            }

        });

        return uniqueSolutions.Count;
    }

    static string NormalizeSolution(int[] solution)
    {
        int n = solution.Length;
        var transformations = new List<string>();

        for (int i = 0; i < 4; i++)
        {
            solution = Rotate90(solution);
            transformations.Add(string.Join(",", solution));
            transformations.Add(string.Join(",", Reflect(solution)));
        }

        transformations.Sort();
        return transformations[0];
    }

    static int[] Rotate90(int[] solution)
    {
        int n = solution.Length;
        int[] rotated = new int[n];
        for (int i = 0; i < n; i++)
        {
            rotated[solution[i]] = n - 1 - i;
        }
        return rotated;
    }

    static int[] Reflect(int[] solution)
    {
        int n = solution.Length;
        int[] reflected = new int[n];
        for (int i = 0; i < n; i++)
        {
            reflected[i] = n - 1 - solution[i];
        }
        return reflected;
    }
}