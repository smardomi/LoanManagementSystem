using LoanManagementSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace LoanManagementSystem.Infrascructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.Firstname)
                .IsRequired()
                .HasMaxLength(200);               

            builder.Property(a => a.Lastname)
                .IsRequired()
                .HasMaxLength(200);

            builder.ToTable(nameof(User),"Identity");
        }
    }
}
