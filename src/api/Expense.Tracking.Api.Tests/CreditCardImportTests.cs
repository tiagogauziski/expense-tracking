using Expense.Tracking.Api.Domain.Engines;

namespace Expense.Tracking.Api.Tests
{
    public class CreditCardImportTests
    {
        [Fact]
        public async Task LoadCreditCardStatementFromDisk()
        {
            var importEngine = new CreditCardStatementLayoutEngine();

            var filePath = @"C:\Dev\TransactionData\9554-xxxx-xxxx-0804_Statement_2023-10-06.csv";
            var transactions = await importEngine.Execute(File.OpenRead(filePath));

            Assert.Equal(16, transactions.Count());
        }
    }
}
