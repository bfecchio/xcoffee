using Microsoft.Extensions.DependencyInjection;

using XCoffee.Core.Domain;
using OrderingServices = XCoffee.Ordering.Infrastructure.IoC;

namespace XCoffee.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        #region Extension Methods

        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            Guard.AssertArgumentNotNull(services, "The services is required.");

            OrderingServices.DependencyInjector.RegisterAll(services);
        }

        #endregion
    }
}
