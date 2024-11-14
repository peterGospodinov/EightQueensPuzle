using QueensPuzzle.Application.Consumer;
using QueensPuzzle.Application.Normalizer;
using QueensPuzzle.Application.Producer;
using QueensPuzzle.Application.Services;

namespace Tests
{
    public class NQueensServiceTests
    {
        private readonly ISolutionProducer producer = new SolutionProducer();
        private readonly SolutionNormalizer normalizer = new SolutionNormalizer();
        private readonly ISolutionConsumer consumer;

        public NQueensServiceTests()
        {
            consumer = new SolutionConsumer(normalizer);
        }

        [Fact]
        public async Task TestTwoQueensResultShouldBeZerroFundamentalAndTotal()
        {
            var service = new NQueensService(producer, consumer);
            var result = await service.SolvePuzle(2);

            Assert.NotNull(result);
            Assert.Equal(0, result.FundamentalSolutionsCount);
            Assert.Equal(0, result.TotalSolutionsCount);
        }

        [Fact]
        public async Task TestEightQueensResult()
        {
            var service = new NQueensService(producer, consumer);
            var result = await service.SolvePuzle(8);

            Assert.NotNull(result);
            Assert.Equal(12, result.FundamentalSolutionsCount);
            Assert.Equal(92, result.TotalSolutionsCount);
        }
    }
}