using innowise_task_server.Data;
using innowise_task_server.Models;
using innowise_task_server.Services;
using Microsoft.EntityFrameworkCore;

namespace innowise_task_server
{
    public static class ServerModule
    {
        public static void AddServerModule(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            services.AddTransient<IFridgeService, FridgeService>();
            services.AddTransient<IServerDbContext, ServerDbContext>();
            services.AddDbContext<ServerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Fridges.Data") ?? throw new InvalidOperationException("Connection string 'Fridges.Data' not found.")));
        }
    }
}
