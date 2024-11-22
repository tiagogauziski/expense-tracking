using Expense.Tracking.Api.Client.Contracts;

namespace Expense.Tracking.Api.Client.Clients;

public class ImportRuleClient(HttpClient httpClient) : BaseRestClient<ImportRule>("/api/importrule", httpClient)
{
}
