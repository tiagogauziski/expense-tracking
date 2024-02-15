using Expense.Tracking.Api.Domain;

namespace Expense.Tracking.Api.UnitTests
{
    public class AnzAccountSummaryTests
    {
        private const int AccountTypeColumn = 0;
        private const int AccountDetailsColumn = 1;
        private const int AccountParticularsColumn = 2;
        private const int AccountCodeColumn = 3;
        private const int AccountReferenceColumn = 4;
        private const int AccountAmountColumn = 5;
        private const int AccountDateColumn = 6;

        [Fact]
        public async Task LoadFromDisk()
        {
            var filePath = @"C:\Dev\TransactionData\06-0222-0838845-00_Transactions_2024-01-14_2024-02-12.csv";

            var isHeader = true;
            var transactionList = new List<Transaction>();

            await foreach(var line in File.ReadLinesAsync(filePath))
            {
                // Skip header
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var lineSplit = line.Split(',');

                var transaction = new Transaction();
                transaction.Type = lineSplit[AccountTypeColumn];
                transaction.Amount = decimal.Parse(lineSplit[AccountAmountColumn]);
                transaction.Date = DateOnly.Parse(lineSplit[AccountDateColumn]);
                if (transaction.Type == "Visa Purchase")
                {
                    transaction.Details = lineSplit[AccountCodeColumn];
                    transaction.Owner = lineSplit[AccountDetailsColumn];
                }
                else if (transaction.Type == "Eft-Pos")
                {
                    transaction.Details = lineSplit[AccountDetailsColumn];
                    transaction.Owner = lineSplit[AccountParticularsColumn] + lineSplit[AccountCodeColumn];
                }
                else if (transaction.Type == "Direct Debit")
                {
                    transaction.Details = lineSplit[AccountDetailsColumn];
                }
                else if (transaction.Type == "Automatic Payment")
                {
                    transaction.Details = lineSplit[AccountDetailsColumn];
                }
                else
                {
                    continue;
                }

                transactionList.Add(transaction);
            }
        }
    }
}