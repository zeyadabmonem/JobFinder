using JobFinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFinder.DAL.Configurations;

public class ApplicationUserConfiguration
    : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // FullName
        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(150);

        // CompanyName
        builder.Property(u => u.CompanyName)
            .HasMaxLength(200);

        // CreatedAt
        builder.Property(u => u.CreatedAt)
            .IsRequired();
    }
}
