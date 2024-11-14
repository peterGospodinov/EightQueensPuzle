using EightQueensPuzleConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace QueensPuzle.Web.Data
{
    public class ResultContext : DbContext
    {
        public ResultContext(DbContextOptions<ResultContext> options) : base(options) { }

        public DbSet<SolutionResult> SolutionResults { get; set; }
    }
}
