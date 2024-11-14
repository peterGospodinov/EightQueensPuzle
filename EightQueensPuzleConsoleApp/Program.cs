using EightQueensPuzleConsoleApp;
using EightQueensPuzleConsoleApp.Models;
using System.Collections.Concurrent;
using System.Diagnostics;

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

        /*
        for (int n = 0; n <= 17; n++)
        {
            var solutions = new BlockingCollection<int[]>();
            var solutionCounter = new ConcurrentBag<int>();
            int fundamentalSolutions = 0;
            var stopwatch = Stopwatch.StartNew();

            ISolutionProducer producer = new SolutionProducer();
            SolutionNormalizer normalizer = new SolutionNormalizer();
            ISolutionConsumer consumer = new SolutionConsumer(normalizer);

            //Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Starting {n}-Queens Problem");

            var producerTask = Task.Run(() => producer.ProduceSolutions(n, solutions, solutionCounter));
            var consumerTask = Task.Run(() =>
            {
                fundamentalSolutions = consumer.CountFundamentalSolutions(solutions);
                stopwatch.Stop();
            });

            await producerTask;
            solutions.CompleteAdding();
            await consumerTask;

            //Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Total Solutions for {n}-Queens: {solutionCounter.Count}\n");
            Console.WriteLine($"Solutions for {n}: Fundamental:  {fundamentalSolutions} | Total: {solutionCounter.Count} | Elapsed Time: {stopwatch.Elapsed} ");
        }
        */
        Console.ReadLine();
    }
}