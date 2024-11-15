namespace QueensPuzzle.Application.Models
{
    /// <summary>
    /// Represents the result of a solution for the N-Queens problem.
    /// </summary>
    public class SolutionResult
    {
        /// <summary>
        /// The unique identifier for the solution result. Also the N value
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The number of fundamental solutions found.
        /// </summary>
        public int FundamentalSolutionsCount { get; set; }

        /// <summary>
        /// The total number of solutions found, including rotations and reflections.
        /// </summary>
        public int TotalSolutionsCount { get; set; }

        /// <summary>
        /// The time taken to compute the solutions, formatted as a string.
        /// </summary>
        public string ElapsedTime { get; set; }
    }
}
