using innowise_task_server.Models;
using Microsoft.EntityFrameworkCore;

namespace innowise_task_server.Data
{
    public class ServerDbContext : DbContext, IServerDbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options)
            : base(options)
        {
        }

        public ServerDbContext()
        {

        }

        public virtual DbSet<Fridge> Fridges { get; set; } = default!;
        public virtual DbSet<Product> Products { get; set; } = default!;
        public virtual DbSet<FridgeModel> FridgeModels { get; set; } = default!;
        public virtual DbSet<FridgeProduct> FridgeProducts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FridgeProduct>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Fridges)
                .HasForeignKey(x => x.ProductID);
            modelBuilder.Entity<FridgeProduct>()
                .HasOne(x => x.Fridge)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.FridgeID);
            modelBuilder.Entity<Fridge>()
                .HasOne(x => x.Model);
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
