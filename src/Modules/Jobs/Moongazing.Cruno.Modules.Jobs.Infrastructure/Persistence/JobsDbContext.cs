using Microsoft.EntityFrameworkCore;
using Moongazing.Cruno.Modules.Jobs.Domain.Entities;

namespace Moongazing.Cruno.Modules.Jobs.Infrastructure.Persistence;

public class JobsDbContext : DbContext
{
    public JobsDbContext(DbContextOptions<JobsDbContext> options) : base(options) { }

    public DbSet<JobDefinition> JobDefinitions => Set<JobDefinition>();
    public DbSet<JobExecutionLog> JobExecutionLogs => Set<JobExecutionLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsDbContext).Assembly);
    }
}
