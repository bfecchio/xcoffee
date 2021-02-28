using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Data.Context;

namespace XCoffee.Web.Configurations
{
    public static class DatabaseConfig
    {
        #region Extension Methods

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Guard.AssertArgumentNotNull(services, "The services is required.");

            services.AddDbContext<OrderingContext>(options
                => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        #endregion
    }
}
