using Expense.Tracking.Api.Client;
using Expense.Tracking.Api.Client.Contracts;

namespace Expenses.Tracking.SystemTests.API.StepDefinitions;

[Binding]
public class CategorySteps(ExpenseTrackingApiClient apiClient)
{
    private HttpResponseMessage response;
    [When("I send a POST request to \\/api\\/category with the following data")]
    public async Task WhenISendAPOSTRequestToApiCategoryWithTheFollowingData(DataTable dataTable)
    {
        response = await apiClient.CreateCategoryAsync(new Category()
        {
            Name = dataTable.Rows[0]["Name"]
        });
    }

    [Then("the response status code should be {int}")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        Assert.AreEqual(statusCode, (int)response.StatusCode);
    }
}
