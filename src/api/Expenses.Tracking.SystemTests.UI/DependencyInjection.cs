using Expenses.Tracking.SystemTests.UI.Pages;
using Expenses.Tracking.SystemTests.UI.Support;
using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Expenses.Tracking.SystemTests.UI
{
    internal static class DependencyInjection
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            // TODO: add your test dependencies here
            services.RegisterPlaywright();
            services.AddSingleton<IWebsite, ExpenseUiWebsite>();
            services.AddSingleton<CategoryPage>();

            return services;
        }

        private static void RegisterPlaywright(this IServiceCollection builder)
        {
            builder.AddSingleton(async provider =>
            {
                var playwright = await Playwright.CreateAsync().ConfigureAwait(false);
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    SlowMo = 200
                }).ConfigureAwait(false);
                return await browser.NewPageAsync();
            });
        }
    }
}
