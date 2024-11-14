using Microsoft.EntityFrameworkCore;
using QueensPuzzle.Application.Models;

namespace QueensPuzle.Web.Data
{
    public class ResultContext : DbContext
    {
        public ResultContext(DbContextOptions<ResultContext> options) : base(options) { }

        public DbSet<SolutionResult> SolutionResults { get; set; }
    }
}
