using Expense.Tracking.Api.Domain.Engines;

namespace Expense.Tracking.Api.Tests;

public class AnzAccountSummaryTests
{
    [Fact]
    public async Task LoadFromDisk()
    {
        var importEngine = new BankCsvLayoutEngine();

        var filePath = @"C:\Dev\TransactionData\06-0222-0838845-00_Transactions_2024-01-14_2024-02-12.csv";
        var transactions = await importEngine.Execute(File.OpenRead(filePath));

        Assert.Equal(97, transactions.Count());
    }
}