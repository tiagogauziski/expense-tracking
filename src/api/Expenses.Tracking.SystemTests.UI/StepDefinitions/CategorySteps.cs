using Expenses.Tracking.SystemTests.UI.Pages;
using Expenses.Tracking.SystemTests.UI.Support;

namespace Expenses.Tracking.SystemTests.UI.StepDefinitions;

[Binding]
internal class CategorySteps(IWebsite website, CategoryPage categoryPage)
{
    [Given("The ExpensesUi page is loaded")]
    public async Task GivenTheExpensesUiPageIsLoaded()
    {
        await categoryPage.GoToPage();
    }

    [When("the Category navigation button is clicked")]
    public async Task WhenTheSupportButtonIsClicked()
    {
        await categoryPage.ClickCategoryButton();
    }

    [Then("The category table is displayed")]
    public async Task ThenTheCategoryTableIsDisplayed()
    {
        Assert.AreEqual("Name", (await website.Page.TextContentAsync("th")).Trim());
    }
}
