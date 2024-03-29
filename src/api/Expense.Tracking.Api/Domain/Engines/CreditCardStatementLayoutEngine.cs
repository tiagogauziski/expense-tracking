using Expense.Tracking.Api.Domain.Models;
using System.Linq.Dynamic.Core;

namespace Expense.Tracking.Api.Domain.Engines;

public class CreditCardStatementLayoutEngine : IEngine
{
    private const int CreditCardCardColumn = 0;
    private const int CreditCardTypeColumn = 1;
    private const int CreditCardAmountColumn = 2;
    private const int CreditCardDetailsColumn = 3;
    private const int CreditCardTransactionDateColumn = 4;
    private const int CreditCardProcessedDateColumn = 5;
    private const int CreditCardForeignCurrencyAmountColumn = 6;
    private const int CreditCardConversionChargeColumn = 7;

    private readonly IEnumerable<ImportRule> importRules;

    public CreditCardStatementLayoutEngine(IEnumerable<ImportRule> importRules)
    {
        this.importRules = importRules;
    }

    public CreditCardStatementLayoutEngine() : this([])
    {
    }

    public async Task<IEnumerable<ImportTransaction>> Execute(Stream stream, CancellationToken cancellationToken = default)
    {
        using StreamReader reader = new StreamReader(stream);

        var transactionList = new List<ImportTransaction>();
        var isHeader = true;

        string? line = string.Empty;
        while (!reader.EndOfStream)
        {
            line = await reader.ReadLineAsync(cancellationToken);
            if (line is null)
            {
                break;
            }

            // Skip header
            if (isHeader)
            {
                isHeader = false;
                continue;
            }

            var transaction = StringToTransaction(line);
            if (transaction is null)
            {
                continue;
            }

            transactionList.Add(transaction);
        }

        return ExecuteImportRules(transactionList);
    }

    internal IEnumerable<ImportTransaction> ExecuteImportRules(IEnumerable<ImportTransaction> importTransactions)
    {
        foreach (var importRule in importRules)
        {
            var query = importTransactions.AsQueryable();
            query = query.Where(importRule.Condition);
            query.ToList().ForEach(transaction =>
            {
                transaction.CategoryId = importRule.CategoryId;
            });
        }

        return importTransactions;
    }

    internal ImportTransaction? StringToTransaction(string text)
    {
        var lineSplit = text.Split(',');

        var transaction = new ImportTransaction();
        if (lineSplit[CreditCardTypeColumn] != "D")
        {
            return null;
        }

        transaction.Type = "Credit Card";
        transaction.Amount = decimal.Parse(lineSplit[CreditCardAmountColumn]);
        transaction.Date = DateOnly.ParseExact(lineSplit[CreditCardTransactionDateColumn], "dd/MM/yyyy");
        transaction.Owner = lineSplit[CreditCardCardColumn];

        const int countryLength = 2;
        const int referenceLength = 14;
        var index = lineSplit[CreditCardDetailsColumn].Length - 1;
        var country = lineSplit[CreditCardDetailsColumn].Substring(index - countryLength, countryLength);
        index -= countryLength;

        var reference = lineSplit[CreditCardDetailsColumn].Substring(index - referenceLength, referenceLength);
        index -= referenceLength;

        var details = lineSplit[CreditCardDetailsColumn].Substring(0, referenceLength);

        transaction.Details = details.Trim();
        transaction.Reference = reference.Trim();

        return transaction;
    }
}
