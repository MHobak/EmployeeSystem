using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Persistence
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
               .ToTable("Employee", t => t.HasComment("Employees table"))
               .HasIndex(m => m.ID)
               .IsUnique();

            builder.Entity<Employee>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasComment("Empleyee name");

            builder.Entity<Employee>()
                .Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(200)
                .HasComment("Empleyee last name");

            builder.Entity<Employee>()
                .Property(m => m.RFC)
                .IsRequired()
                .HasMaxLength(13)
                .HasComment("Empleyee RFC code");

            builder.Entity<Employee>()
                .Property(m => m.BornDate)
                .IsRequired()
                .HasComment("Employee born date");

            builder.Entity<Employee>()
                .Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue(EmployeeStatus.NotSet)
                .HasComment("Employee status")
                .HasConversion<string>(); //save as string
        }
    }
}
