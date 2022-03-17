using Microsoft.EntityFrameworkCore;
namespace Division2ReconService.Data
{
    public class Division2ReconDbContext : DbContext
    {
        public Division2ReconDbContext(DbContextOptions<Division2ReconDbContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }


        }

        public DbSet<Customer> Customers { get; set; }
    }
}
