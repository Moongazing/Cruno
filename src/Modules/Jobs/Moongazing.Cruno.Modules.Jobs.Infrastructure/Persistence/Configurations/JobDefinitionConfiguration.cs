using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Cruno.Modules.Jobs.Domain.Entities;

namespace Moongazing.Cruno.Modules.Jobs.Infrastructure.Persistence.Configurations;

public class JobDefinitionConfiguration : IEntityTypeConfiguration<JobDefinition>
{
    public void Configure(EntityTypeBuilder<JobDefinition> builder)
    {
        builder.ToTable("JobDefinitions");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(j => j.CronExpression)
            .IsRequired();

        builder.Property(j => j.Payload)
            .IsRequired();

        builder.Property(j => j.MaxRetryCount)
            .IsRequired();

        builder.Property(j => j.RetryStrategy)
            .IsRequired();

        builder.Property(j => j.IsActive)
            .HasDefaultValue(true);

        builder.Property(j => j.CreatedAt)
            .IsRequired();
    }
}
