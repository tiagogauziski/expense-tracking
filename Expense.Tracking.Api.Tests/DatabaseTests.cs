using Expense.Tracking.Api.Infrastrucure.Database;

namespace Expense.Tracking.Api.Tests;

public class DatabaseTests
{
    [Fact]
    public async Task SaveDataIntoDatabase()
    {
        var context = new ExpenseContext();

        // Ensure database gets created
        await context.Database.EnsureCreatedAsync();

    }
}
