
using JobFinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFinder.DAL.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        // Primary Key
        builder.HasKey(j => j.Id);

        // Properties
        builder.Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(j => j.CompanyName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(j => j.Salary)
            .HasColumnType("decimal(18,2)");

        builder.ToTable(table => 
        {
            table.HasCheckConstraint("CK_Jobs_Salary_NonNegative", "[Salary] >= 0");
            table.HasCheckConstraint("CK_Jobs_YearsOfExperience_NonNegative", "[YearsOfExperience] >= 0");
        });

        builder.Property(j => j.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(j => j.OwnerId)
            .IsRequired();

        builder.Property(j => j.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        // Job -> Owner (ApplicationUser)
        builder.HasOne(j => j.Owner)
            .WithMany(u => u.Jobs)
            .HasForeignKey(j => j.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

      
    }
}