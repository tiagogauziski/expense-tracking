using Microsoft.Playwright;

namespace Expenses.Tracking.SystemTests.UI.Support
{
    internal class ExpenseUiWebsite(Task<IPage> page) : IWebsite
    {
        public IPage Page { get; set; }

        public async Task Navigate(string relativePath)
        {
            Page = await page;

            var uri = new Uri(new Uri("http://localhost:8081/"), relativePath);

            await Page.GotoAsync(uri.AbsoluteUri);
        }
    }
}
