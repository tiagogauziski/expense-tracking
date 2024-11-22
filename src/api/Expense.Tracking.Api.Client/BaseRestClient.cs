using System.Net.Http.Json;

namespace Expense.Tracking.Api.Client
{
    public abstract class BaseRestClient<TEntity> where TEntity : class
    {
        private readonly string entityUri;
        private readonly HttpClient httpClient;

        protected BaseRestClient(string entityUri, HttpClient httpClient)
        {
            ArgumentException.ThrowIfNullOrEmpty(entityUri, nameof(entityUri));
            ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));

            this.entityUri = entityUri;
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(int id)
        {
            return await httpClient.GetAsync($"{entityUri}/{id}");
        }

        public async Task<IList<TEntity>?> GetAsync()
        {
            var response = await httpClient.GetAsync(entityUri);
            return await response.Content.ReadFromJsonAsync<IList<TEntity>>();
        }

        public async Task<HttpResponseMessage> CreateAsync(TEntity category)
        {
            return await httpClient.PostAsJsonAsync(entityUri, category);
        }

        public async Task<HttpResponseMessage> GetAsync(int id, TEntity category)
        {
            return await httpClient.PutAsJsonAsync($"{entityUri}/{id}", category);
        }

        public async Task<HttpResponseMessage> DeletAsync(int id)
        {
            return await httpClient.DeleteAsync($"{entityUri}/{id}");
        }
    }
}
