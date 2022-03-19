using Microsoft.EntityFrameworkCore;
namespace Division2ReconService.Data;

/// <summary>
/// 
/// </summary>
public class Division2ReconDbContext : DbContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public Division2ReconDbContext(DbContextOptions<Division2ReconDbContext> options)
        : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
        }
    }

    /// <summary>
    /// Customers Dbset
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Machines Dbset
    /// </summary>
    public DbSet<Machine> Machines { get; set; }
}
