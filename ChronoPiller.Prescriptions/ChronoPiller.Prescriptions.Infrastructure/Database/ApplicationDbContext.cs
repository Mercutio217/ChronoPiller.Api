using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChronoPiller.Infrastructure.Database;

[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
public class ApplicationDbContext : DbContext
{
    private readonly DatabaseOptions _databaseOptions;
    public ApplicationDbContext(IOptions<DatabaseOptions> options)
    {
        _databaseOptions = options.Value;
    }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medication> Medications { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_databaseOptions.ChronoPiller, options => options.EnableRetryOnFailure());
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Prescription>().HasKey(p => p.Id);
        builder.Entity<Prescription>().HasMany(p => p.Items)
            .WithOne().HasForeignKey(p => p.PrescriptionId);
        builder.Entity<Prescription>().ToTable("Prescriptions");
        builder.Entity<Medication>().HasKey(p => p.Id);
        builder.Entity<Medication>().ToTable("PrescriptionItems");
        builder.Entity<Medication>().HasMany(p => p.Doses).WithOne();
        builder.Entity<Dosage>().HasKey(d => d.Id);

        base.OnModelCreating(builder);
    }
}