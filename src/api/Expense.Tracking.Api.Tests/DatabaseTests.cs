﻿using Expense.Tracking.Api.Domain.Engines;
using Expense.Tracking.Api.Domain.Models;
using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Expense.Tracking.Api.Tests;

public class DatabaseTests
{
    private ExpenseContext _expenseContext;

    public DatabaseTests()
    {
        var options = new DbContextOptionsBuilder<ExpenseContext>()
            .UseInMemoryDatabase(databaseName: "ExpenseTracking")
            .Options;

        _expenseContext = new ExpenseContext(options);
        _expenseContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task SaveDataIntoDatabase()
    {
        var importEngine = new BankCsvLayoutEngine();

        var filePath = @"C:\Dev\TransactionData\06-0222-0838845-00_Transactions_2024-01-14_2024-02-12.csv";
        var transactions = await importEngine.Execute(File.OpenRead(filePath));

        var import = new Import()
        {
            Name = "unittest",
            Layout = nameof(BankCsvLayoutEngine),
            Transactions = transactions.ToList()
        };

        await _expenseContext.AddAsync(import);

        await _expenseContext.SaveChangesAsync();

        var imports = _expenseContext.Imports.ToList();

        Assert.Single(imports);
        Assert.Equal(103, imports.First().Transactions.Count);
    }

    [Fact]
    public async Task ApplyImportRules()
    {
        int rentId = 1;
        var importRuleList = new List<ImportRule>
        {
            new ImportRule
            {
                Name = "Rent",
                Condition = "Type == \"Automatic Payment\" && Details.Contains(\"Faceup Rent\")",
                CategoryId = rentId
            }
        };

        var importEngine = new BankCsvLayoutEngine(importRuleList);

        var filePath = @"C:\Dev\TransactionData\06-0222-0838845-00_Transactions_2024-01-14_2024-02-12.csv";
        var transactions = await importEngine.Execute(File.OpenRead(filePath));

        Assert.Equal(4, transactions.Where(transaction => transaction.CategoryId == rentId).Count());
    }
}
