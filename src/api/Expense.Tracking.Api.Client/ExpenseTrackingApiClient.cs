using Expense.Tracking.Api.Client.Contracts;
using System.Net.Http.Json;

namespace Expense.Tracking.Api.Client;

public class ExpenseTrackingApiClient(HttpClient httpClient)
{
    public async Task<HttpResponseMessage> GetCategoryAsync(int id)
    {
        return await httpClient.GetAsync($"/api/category/{id}");
    }

    public async Task<IList<Category>?> GetCategoriesAsync()
    {
        var response = await httpClient.GetAsync("/api/category");
        return await response.Content.ReadFromJsonAsync<IList<Category>>();
    }

    public async Task<HttpResponseMessage> CreateCategoryAsync(Category category)
    {
        return await httpClient.PostAsJsonAsync("/api/category", category);
    }

    public async Task<HttpResponseMessage> UpdateCategoryAsync(int id, Category category)
    {
        return await httpClient.PutAsJsonAsync($"/api/category/{id}", category);
    }

    public async Task<HttpResponseMessage> DeleteCategoryAsync(int id)
    {
        return await httpClient.DeleteAsync($"/api/category/{id}");
    }
}
