using QueensPuzzle.Application.Consumer;
using QueensPuzzle.Application.Models;
using QueensPuzzle.Application.Normalizer;
using QueensPuzzle.Application.Producer;
using QueensPuzzle.Application.Services;

public class Program
{
    static async Task Main()
    {
        int n = 8;
        ISolutionProducer producer = new SolutionProducer();
        SolutionNormalizer normalizer = new SolutionNormalizer();
        ISolutionConsumer consumer = new SolutionConsumer(normalizer);

        var service = new NQueensService(producer, consumer);
        SolutionResult solutionResult = await service.SolvePuzle(n);

        Console.WriteLine($"Solutions for {n}: Fundamental:  {solutionResult.FundamentalSolutionsCount} | Total: {solutionResult.TotalSolutionsCount} | Elapsed Time: {solutionResult.ElapsedTime} ");

        Console.ReadLine();
    }
}