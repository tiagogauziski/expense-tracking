using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Expenses.Tracking.SystemTests.API
{
    internal static class DependencyInjection
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            // TODO: add your test dependencies here

            return services;
        }
    }
}
