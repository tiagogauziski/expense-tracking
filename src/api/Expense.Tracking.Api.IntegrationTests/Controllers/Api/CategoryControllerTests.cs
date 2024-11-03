using Expense.Tracking.Api.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Text;

namespace Expense.Tracking.Api.IntegrationTests.Controllers.Api
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        public async Task GetCategoryTest()
        {
            // Arrange
            var factory = new CustomWebApplicationFactory<Program>();
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/category");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task PostCategoryTest()
        {
            // Arrange
            var factory = new CustomWebApplicationFactory<Program>();
            var client = factory.CreateClient();
            var newCategory = new Category
            {
                Name = "New Category",
            };
            var content = new StringContent(JsonConvert.SerializeObject(newCategory), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/category", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var createdCategory = JsonConvert.DeserializeObject<Category>(responseString);
            Assert.AreEqual(newCategory.Name, createdCategory.Name);
        }

        [TestMethod]
        public async Task PostAndGetCategoryTest()
        {
            // Arrange
            var factory = new CustomWebApplicationFactory<Program>();
            var client = factory.CreateClient();
            var newCategory = new Category
            {
                Name = "New Category",
            };
            var content = new StringContent(JsonConvert.SerializeObject(newCategory), Encoding.UTF8, "application/json");

            // Act
            var postResponse = await client.PostAsync("/api/category", content);
            postResponse.EnsureSuccessStatusCode();
            var createdCategory = JsonConvert.DeserializeObject<Category>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"/api/category/{createdCategory.Id}");
            getResponse.EnsureSuccessStatusCode();
            var responseString = await getResponse.Content.ReadAsStringAsync();
            var categoryResponse = JsonConvert.DeserializeObject<Category>(responseString);

            // Assert
            Assert.AreEqual(newCategory.Name, categoryResponse.Name);
        }

        [TestMethod]
        public async Task PostDuplicateCategoryTest()
        {
            // Arrange
            var factory = new CustomWebApplicationFactory<Program>();
            var client = factory.CreateClient();
            var newCategory = new Category
            {
                Name = "Duplicate Category",
            };
            var content = new StringContent(JsonConvert.SerializeObject(newCategory), Encoding.UTF8, "application/json");

            // Act
            var firstResponse = await client.PostAsync("/api/category", content);
            firstResponse.EnsureSuccessStatusCode();

            var secondResponse = await client.PostAsync("/api/category", content);

            // Assert
            Assert.IsFalse(secondResponse.IsSuccessStatusCode);
            Assert.AreEqual(System.Net.HttpStatusCode.Conflict, secondResponse.StatusCode);
        }

        [TestMethod]
        public async Task UpdateCategoryTest()
        {
            // Arrange
            var factory = new CustomWebApplicationFactory<Program>();
            var client = factory.CreateClient();
            var newCategory = new Category
            {
                Name = "New Category",
            };
            var content = new StringContent(JsonConvert.SerializeObject(newCategory), Encoding.UTF8, "application/json");

            // Act
            var postResponse = await client.PostAsync("/api/category", content);
            postResponse.EnsureSuccessStatusCode();
            var createdCategory = JsonConvert.DeserializeObject<Category>(await postResponse.Content.ReadAsStringAsync());

            // Update the category
            createdCategory.Name = "Updated Category";
            var updateContent = new StringContent(JsonConvert.SerializeObject(createdCategory), Encoding.UTF8, "application/json");
            var putResponse = await client.PutAsync($"/api/category/{createdCategory.Id}", updateContent);

            // Assert
            putResponse.EnsureSuccessStatusCode();

            // Verify the update
            var getResponse = await client.GetAsync($"/api/category/{createdCategory.Id}");
            getResponse.EnsureSuccessStatusCode();
            var responseString = await getResponse.Content.ReadAsStringAsync();
            var updatedCategory = JsonConvert.DeserializeObject<Category>(responseString);

            Assert.AreEqual("Updated Category", updatedCategory.Name);
        }


    }
}
