using innowise_task_server.Models;
using Microsoft.EntityFrameworkCore;

namespace innowise_task_server.Data
{
    public interface IServerDbContext : IDisposable
    {
        DbSet<Fridge> Fridges { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<FridgeModel> FridgeModels { get; set; }

        DbSet<FridgeProduct> FridgeProducts { get; set; }

        int SaveChanges();
    }
}
