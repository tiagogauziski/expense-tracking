using Expense.Tracking.Api.Domain.Engines;
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

        var importEngine = new BankCsvLayoutEngine();

        var filePath = @"C:\Dev\TransactionData\06-0222-0838845-00_Transactions_2024-01-14_2024-02-12.csv";
        var transactions = await importEngine.Execute(File.OpenRead(filePath));

        await context.AddRangeAsync(transactions);

        await context.SaveChangesAsync();

    }
}
