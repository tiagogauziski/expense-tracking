using Expense.Tracking.Api.Client.Clients;

namespace Expense.Tracking.Api.Client;

public class ExpenseTrackingApiClient(HttpClient httpClient)
{
    public CategoryClient Category => new(httpClient);

    public ImportRuleClient ImportRule => new(httpClient);
}
