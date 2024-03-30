using Expense.Tracking.Api.Domain.Models;
using System.Linq.Dynamic.Core;

namespace Expense.Tracking.Api.Domain.Engines;

public class BankCsvLayoutEngine : IEngine
{
    private const int AccountTypeColumn = 0;
    private const int AccountDetailsColumn = 1;
    private const int AccountParticularsColumn = 2;
    private const int AccountCodeColumn = 3;
    private const int AccountReferenceColumn = 4;
    private const int AccountAmountColumn = 5;
    private const int AccountDateColumn = 6;

    private readonly IEnumerable<ImportRule> importRules;

    public BankCsvLayoutEngine(IEnumerable<ImportRule> importRules)
    {
        this.importRules = importRules;
    }

    public BankCsvLayoutEngine() : this([])
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

        return ApplyRules(transactionList);
    }

    public IEnumerable<ImportTransaction> ApplyRules(IEnumerable<ImportTransaction> importTransactions)
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

    internal ImportTransaction StringToTransaction(string text)
    {
        var lineSplit = text.Split(',');

        var transaction = new ImportTransaction();
        transaction.Type = lineSplit[AccountTypeColumn];
        transaction.Amount = decimal.Parse(lineSplit[AccountAmountColumn]);
        transaction.Date = DateOnly.ParseExact(lineSplit[AccountDateColumn], "dd/MM/yyyy");
        switch (transaction.Type)
        {
            case "Visa Purchase":
                transaction.Details = lineSplit[AccountCodeColumn];
                transaction.Owner = lineSplit[AccountDetailsColumn];
                break;
            case "Eft-Pos":
                transaction.Details = lineSplit[AccountDetailsColumn];
                transaction.Owner = lineSplit[AccountParticularsColumn] + lineSplit[AccountCodeColumn];
                transaction.Reference = lineSplit[AccountReferenceColumn];
                break;
            case "Direct Debit":
                transaction.Details = lineSplit[AccountDetailsColumn];
                break;
            case "Payment":
                transaction.Details = lineSplit[AccountDetailsColumn];
                break;
            case "Automatic Payment":
                transaction.Details = lineSplit[AccountDetailsColumn];
                break;
            default:
                return null;
        }

        return transaction;
    }
}
