using Ordering.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Orders.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();

                    if (orderContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        orderContext.Database.Migrate();

                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return host;
        }
    }
}
