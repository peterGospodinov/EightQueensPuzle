using Microsoft.EntityFrameworkCore;
using QueensPuzle.Web.Data;
namespace NQueensPuzzle.Web.Services
{
    public class DbMigrationService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbMigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
      
        public void ApplyMigrations()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ResultContext>();

            try
            {
                Console.WriteLine("Applying migrations...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrations applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to apply migrations: {ex.Message}");
                throw; // Rethrow the exception to ensure app doesn't start with a broken database
            }
        }
    }
}
