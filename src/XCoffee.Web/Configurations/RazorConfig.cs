using Microsoft.Extensions.DependencyInjection;

using XCoffee.Core.Domain;

namespace XCoffee.Web.Configurations
{
    public static class RazorConfig
    {
        #region Extension Methods

        public static IServiceCollection AddRazorConfiguration(this IServiceCollection services)
        {
            Guard.AssertArgumentNotNull(services, "The services is required.");

            services.AddRazorPages();

            return services;
        }

        #endregion
    }
}
