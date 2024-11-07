using Microsoft.Playwright;

namespace Expenses.Tracking.SystemTests.UI.Support
{
    internal interface IWebsite
    {
        IPage Page { get; set; }

        Task Navigate(string relativePath);
    }
}
