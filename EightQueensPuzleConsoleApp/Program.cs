using EightQueensPuzleConsoleApp;
using System.Collections.Concurrent;

public class Program
{
    static async Task Main()
    {
        Console.WriteLine("Work in progress");
        for (int n = 0; n <= 8; n++)
        {
            var solutions = new BlockingCollection<int[]>();
            var solutionCounter = new ConcurrentBag<int>();

            ISolutionProducer producer = new SolutionProducer();
            SolutionNormalizer normalizer = new SolutionNormalizer();
            ISolutionConsumer consumer = new SolutionConsumer(normalizer);

            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Starting {n}-Queens Problem");

            var producerTask = Task.Run(() => producer.ProduceSolutions(n, solutions, solutionCounter));
            var consumerTask = Task.Run(() =>
            {
                int fundamentalSolutions = consumer.CountFundamentalSolutions(solutions);
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Total Fundamental Solutions for {n}-Queens: {fundamentalSolutions}\n");
            });

            await producerTask;
            solutions.CompleteAdding();
            await consumerTask;

            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Total Solutions for {n}-Queens: {solutionCounter.Count}\n");

        }

        Console.ReadLine();
    }
}