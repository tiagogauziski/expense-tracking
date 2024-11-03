using Reqnroll;

namespace Expenses.Tracking.SystemTests.StepDefinitions
{
    [Binding]
    public class CategorySteps
    {
        [When("I send a POST request to \\/api\\/category with the following data")]
        public void WhenISendAPOSTRequestToApiCategoryWithTheFollowingData(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Then("the response status code should be {int}")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            throw new PendingStepException();
        }
    }
}
