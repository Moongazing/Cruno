using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Cruno.Modules.Jobs.Domain.Entities;

namespace Moongazing.Cruno.Modules.Jobs.Infrastructure.Persistence.Configurations;

public class JobExecutionLogConfiguration : IEntityTypeConfiguration<JobExecutionLog>
{
    public void Configure(EntityTypeBuilder<JobExecutionLog> builder)
    {
        builder.ToTable("JobExecutionLogs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.JobId)
            .IsRequired();

        builder.Property(e => e.ExecutedAt)
            .IsRequired();

        builder.Property(e => e.Success)
            .IsRequired();

        builder.Property(e => e.ErrorMessage)
            .HasMaxLength(1000);

        builder.Property(e => e.RetryCount)
            .IsRequired();

        builder.Property(e => e.Duration)
            .IsRequired();
    }
}
