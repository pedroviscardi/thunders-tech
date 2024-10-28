using Microsoft.EntityFrameworkCore;
using Thunders.Tecnologia.Domain.Entities;

namespace Thunders.Tecnologia.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Peoples { get; set; }

    /// <summary>
    ///     On model creating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Email as a unique index
        modelBuilder.Entity<Person>()
            .HasIndex(p => p.Email)
            .IsUnique();

        // Seed data for Person table with a fixed GUID
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = new Guid("b76aeea0-5ddf-4f21-8dbd-5a8a18c7f9d0"),
            Name = "Pedro Paulo Orasmo Viscardi",
            DateOfBirth = new DateTime(1987, 10, 29, 0, 0, 0, DateTimeKind.Utc),
            Email = "pedro@viscarditecnologia.com.br"
        });
    }
}