using System.Diagnostics.CodeAnalysis;
using ChronoPiller.Authorization.Core.Entities;
using ChronoPiller.Authorization.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChronoPiller.Authorization.Infrastructure.Database;

public class ApplicationDbContext(IOptions<DatabaseOptions> options) : DbContext
{
    private readonly DatabaseOptions _databaseOptions = options.Value;
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_databaseOptions.ChronoPiller, options => options.EnableRetryOnFailure());
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
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
            .HasIndex(u => u.UserName)
            .IsUnique();

        base.OnModelCreating(builder);
    }
}