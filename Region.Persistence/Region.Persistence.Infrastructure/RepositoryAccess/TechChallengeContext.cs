using Microsoft.EntityFrameworkCore;

namespace Region.Persistence.Infrastructure.RepositoryAccess;
public class TechChallengeContext(DbContextOptions<TechChallengeContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechChallengeContext).Assembly);
    }
}