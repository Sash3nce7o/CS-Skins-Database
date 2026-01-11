using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Skins.Infrastructure.Data.Models;

namespace Skins.Infrastructure.Data;

public class SkinsDbContext : DbContext
{
<<<<<<< HEAD
    private const string ConnectionString = "Server=localhost, 1433;Database=CS-Skins;User Id=sa;Password=StrongPassword123!;TrustServerCertificate=True;Encrypt=True";
=======
    private const string ConnectionString = "Server=, 1433;Database=CS-Skins;User Id=sa;Password=;TrustServerCertificate=True;Encrypt=True";
>>>>>>> dbac6b992a10563442940e28b0e39d8d1de8cb2c

    public DbSet<Skin> Skins { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString)
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        /*
            Line	Meaning
            HasOne(s => s.Owner)	"A Skin has one Owner" (navigation property)
            WithMany(u => u.Skins)	"A User has many Skins" (inverse navigation)
            HasForeignKey(s => s.OwnerId)	"The link column is OwnerId"
            OnDelete(DeleteBehavior.Cascade)	"If user deleted → auto-delete their skins"
            */
        modelBuilder.Entity<Skin>()
            .HasOne(s => s.Owner)
            .WithMany(u => u.Skins)
            .HasForeignKey(s => s.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);


            
        modelBuilder.Entity<Skin>()
            .Property(s => s.Quality)
            .HasConversion<string>(); 
    }
}
