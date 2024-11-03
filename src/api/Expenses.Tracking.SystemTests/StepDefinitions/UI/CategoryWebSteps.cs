using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace Expenses.Tracking.SystemTests.StepDefinitions.UI
{
    [Binding]
    internal class CategoryWebSteps
    {
        IPage _page;

        [Given("The ExpensesUi page is loaded")]
        public async Task GivenTheExpensesUiPageIsLoaded()
        {
            _page = await InitialisePlaywright();

            await _page.GotoAsync("http://localhost:8081/");
        }

        [When("The support button is clicked")]
        public async Task WhenTheSupportButtonIsClicked()
        {
            await _page.ClickAsync("text=Categories");
        }

        [Then("The category table is displayed")]
        public async Task ThenTheCategoryTableIsDisplayed()
        {
            Assert.AreEqual("Name", (await _page.TextContentAsync("th")).Trim());
        }

        private async Task<IPage> InitialisePlaywright()
        {
            var playwright = await Playwright.CreateAsync().ConfigureAwait(false);
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 200
            }).ConfigureAwait(false);
            return await browser.NewPageAsync().ConfigureAwait(false);
        }
    }
}
