using Microsoft.EntityFrameworkCore;

namespace Moongazing.Cruno.Infrastructure.Persistence;

public class CrunoDbContext : DbContext
{
    public CrunoDbContext(DbContextOptions<CrunoDbContext> options)
        : base(options) { }

    // Geçici olarak boş. Modüller extension olarak ekleyecek:
    // protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    //     modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrunoDbContext).Assembly);
}
