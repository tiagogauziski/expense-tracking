using Expenses.Tracking.SystemTests.UI.Support;

namespace Expenses.Tracking.SystemTests.UI.Pages;

internal class CategoryPage(IWebsite website)
{
    public async Task GoToPage()
    {
        await website.Navigate("/category");
    }

    public async Task ClickCategoryButton()
    {
        await website.Page.ClickAsync("text=Categories");
    }
}
