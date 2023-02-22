using FunBooksAndVideos.Repository.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace FunBooksAndVideos.Repository.Configuration
{
    public static class Configuration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
        }
    }
}
