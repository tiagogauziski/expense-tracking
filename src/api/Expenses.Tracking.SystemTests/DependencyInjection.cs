using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Reqnroll.Infrastructure;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Expenses.Tracking.SystemTests
{
    internal static class DependencyInjection
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            // TODO: add your test dependencies here
            services.RegisterPlaywright();

            return services;
        }

        private static void RegisterPlaywright(this IServiceCollection builder)
        {

        }
    }
}
