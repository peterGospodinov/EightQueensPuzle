namespace QueensPuzzle.Application.Models
{
    public class SolutionResult
    {
        public int Id { get; set; }
        public int FundamentalSolutionsCount { get; set; }
        public int TotalSolutionsCount { get; set; }
        public string ElapsedTime { get; set; }
    }
}
