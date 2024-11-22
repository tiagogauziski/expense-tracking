using Expense.Tracking.Api.Client.Contracts;

namespace Expense.Tracking.Api.Client.Clients;

public class CategoryClient(HttpClient httpClient) : BaseRestClient<Category>("/api/category", httpClient)
{
}
