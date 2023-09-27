using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProfitabilityCalculator.Models;

namespace ProfitabilityCalculator.Database;

public class ProbabilityCalcDBContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ProbabilityCalcDBContext(DbContextOptions<ProbabilityCalcDBContext> dbContextOptions) : base(dbContextOptions)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
        }
        catch (Exception e)
        {
            Console.Write(e);
        }
    }
}