
using JobFinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFinder.DAL.Configurations;

public class JobApplicationConfiguration
    : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        // Primary Key
        builder.HasKey(a => a.Id);

        // Properties
        builder.Property(a => a.SeekerId)
            .IsRequired();

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        // JobApplication -> Job
        builder.HasOne(a => a.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        // JobApplication -> ApplicationUser (Seeker)
        builder.HasOne(a => a.Seeker)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.SeekerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => new { a.SeekerId, a.JobId })
         .IsUnique();
    }
}
