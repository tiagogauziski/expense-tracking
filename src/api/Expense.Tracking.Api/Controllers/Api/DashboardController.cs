﻿using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.Mvc;

namespace Expense.Tracking.Api.Controllers.Api;

[Route("api/dashboard")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly ExpenseContext _context;

    public DashboardController(ExpenseContext context)
    {
        _context = context;
    }

    [HttpGet("summary-year/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<DashboardChartContract> GetTransactionsSummary([FromRoute] string year)
    {
        var query = _context.Transactions
            .Where(transaction => transaction.Date.Year.ToString() == year)
            .GroupBy(transaction => transaction.Category)
            .ToList()
            .Select(group => new DashboardChartContract
            {
                Name = group.Key.Name,
                Series = _context.Transactions
                    .Where(transaction => transaction.Date.Year.ToString() == year && transaction.CategoryId == group.Key.Id)
                    .GroupBy(transaction => transaction.Date.Month)
                    .OrderBy(transaction => transaction.Key)
                    .Select(monthGroup => new DashboardChartSeriesContract
                    {
                        Name = monthGroup.Key.ToString(),
                        Value = monthGroup.Sum(transaction => transaction.Amount)
                    })
            });



        return query;
    }

    [HttpGet("summary-year/{year}/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<DashboardChartSeriesContract> GetTransactionsGroupByCategory([FromRoute] string year, [FromRoute] string category)
    {
        var query = _context.Transactions
            .Where(transaction => transaction.Date.Year.ToString() == year && transaction.Category.Name == category)
            .GroupBy(transaction => transaction.Date.Month)
            .OrderBy(transaction => transaction.Key)
            .Select(monthGroup => new DashboardChartSeriesContract
            {
                Name = monthGroup.Key.ToString(),
                Value = monthGroup.Sum(transaction => transaction.Amount)
            })
            .ToList();

        return query;
    }

    public class DashboardChartSeriesContract
    {
        public string Name { get; set; }

        public decimal Value { get; set; }
    }

    public class DashboardChartContract
    {
        public string Name { get; set; }

        public IEnumerable<DashboardChartSeriesContract> Series { get; set; }
    }
}
