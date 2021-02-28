using Microsoft.Extensions.DependencyInjection;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Data.Context;
using XCoffee.Ordering.Domain.Services;
using XCoffee.Ordering.Data.Repositories;
using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.Repositories;

namespace XCoffee.Ordering.Infrastructure.IoC
{
    public static class DependencyInjector
    {
        #region Extension Methods

        public static IServiceCollection RegisterAll(this IServiceCollection services)
        {
            Guard.AssertArgumentNotNull(services, "The services is required.");

            services
                .RegisterData()
                .RegisterDomainServices();

            return services;
        }

        #endregion

        #region Methods

        private static IServiceCollection RegisterData(this IServiceCollection services)
        {
            services.AddScoped<OrderingContext>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        private static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {            
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IBasketService, BasketService>();

            return services;
        }

        #endregion
    }
}
