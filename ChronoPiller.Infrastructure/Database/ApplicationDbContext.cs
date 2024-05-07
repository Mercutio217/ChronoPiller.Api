using System.Text.Json;
using System.Text.Json.Serialization;
using ChronoPiller.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChronoPiller.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Prescription> Prescriptions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ChronoPillerDb");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        builder.Entity<Prescription>().HasKey(p => p.Id);
        builder.Entity<PrescriptionItem>().HasKey(p => p.Id);
        builder.Entity<PrescriptionItem>().Property(p => p.Times).HasConversion(
            prop => JsonSerializer.Serialize(prop, new JsonSerializerOptions { }), 
            prop => JsonSerializer.Deserialize<List<TimeSpan>>(prop, new JsonSerializerOptions { }));
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().HasMany(p => p.Prescriptions)
            .WithOne().HasForeignKey(p => p.UserId);
        base.OnModelCreating(builder);
    }
}