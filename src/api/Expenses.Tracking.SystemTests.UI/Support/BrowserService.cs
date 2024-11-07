using Microsoft.Playwright;

namespace Expenses.Tracking.SystemTests.UI.Support
{
    internal class BrowserService
    {
        private readonly Task<IPage> page;

        public BrowserService(Task<IPage> page)
        {
            this.page = page;
        }
    }
}
