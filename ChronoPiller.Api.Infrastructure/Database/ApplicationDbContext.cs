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
    public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<NotificationSchedule> NotificationSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=ChronoPillerDb;TrustServerCertificate=true;Integrated Security=SSPI;", options => options.EnableRetryOnFailure());
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Prescription>().HasKey(p => p.Id);
        builder.Entity<Prescription>().HasMany(p => p.Items)
            .WithOne().HasForeignKey(p => p.PrescriptionId);
        builder.Entity<Prescription>().ToTable("Prescriptions");
        
        builder.Entity<PrescriptionItem>().HasKey(p => p.Id);
        builder.Entity<PrescriptionItem>().HasOne(p => p.NotificationSchedule);
        builder.Entity<PrescriptionItem>().ToTable("PrescriptionItems");
        builder.Entity<PrescriptionItem>().HasMany(p => p.Doses).WithOne();
        builder.Entity<Dosage>().HasKey(d => d.Id);
        builder.Entity<NotificationSchedule>().HasKey(n => n.Id);

        builder.Entity<User>()
            .Property(us => us.Email)
            .HasMaxLength(50);
        
        builder.Entity<User>()
            .Property(us => us.Name)
            .HasMaxLength(50);
        
        builder.Entity<User>()
            .Property(us => us.Surname)
            .HasMaxLength(50);  
        
        builder.Entity<User>()
            .Property(us => us.UserName)
            .HasMaxLength(50);
        
        builder.Entity<User>()
            .HasMany(ent => ent.Roles)
            .WithMany();
        
        builder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        builder.Entity<Role>()
            .HasKey(role => role.Id);

        builder.Entity<Role>()
            .HasIndex(role => role.Name)
            .IsUnique();

        builder.Entity<Role>()
            .Property(r => r.Name)
            .HasMaxLength(25);

        base.OnModelCreating(builder);
    }
}