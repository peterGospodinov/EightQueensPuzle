using EightQueensPuzleConsoleApp;
using EightQueensPuzleConsoleApp.Models;
using QueensPuzle.Application.Consumer;
using QueensPuzle.Application.Normalizer;
using QueensPuzle.Application.Producer;

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