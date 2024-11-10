using Expense.Tracking.Api.Client;
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

            services.AddHttpClient<ExpenseTrackingApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080/");
            });

            // TODO: add your test dependencies here

            return services;
        }
    }
}
