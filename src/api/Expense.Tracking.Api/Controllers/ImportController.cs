using Expense.Tracking.Api.Contracts;
using Expense.Tracking.Api.Domain.Engines;
using Expense.Tracking.Api.Domain.Models;
using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Controllers;

[Route("api/import")]
[ApiController]
public class ImportController : ControllerBase
{
    private readonly ExpenseContext _context;

    public ImportController(ExpenseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Import>>> GetImports()
    {
        return await _context.Imports.ToListAsync();
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Import>> GetImport(int id)
    {
        var import = await _context.Imports
            .Include(import => import.Transactions)
            .ThenInclude(transaction => transaction.Category)
            .Where(import => import.Id == id)
            .FirstOrDefaultAsync();

        if (import == null)
        {
            return NotFound();
        }

        return import;
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutImport(int id, Import import)
    {
        if (id != import.Id)
        {
            return BadRequest();
        }

        _context.Entry(import).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ImportExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<ActionResult<Import>> PostImport([FromForm] CreateImport request, CancellationToken cancellationToken)
    {
        if (request.File is null)
        {
            return BadRequest("File is required");
        }
        
        var import = new Import
        {
            Name = request.Name,
            Layout = request.Layout,
            CreatedAt = DateTimeOffset.UtcNow
        };

        IEngine engine;
        switch (request.Layout)
        {
            case "bank-anz-checking-csv-v1":
                engine = new BankCsvLayoutEngine(await _context.ImportRule.ToListAsync());
                break;
            case "bank-anz-creditcard-csv-v1":
                engine = new CreditCardStatementLayoutEngine(await _context.ImportRule.ToListAsync());
                break;
            default:
                return BadRequest("Invalid layout");
        }

        var transactions = await engine.Execute(request.File.OpenReadStream(), cancellationToken);
        import.Transactions = transactions.ToList();

        _context.Imports.Add(import);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetImport", new { id = import.Id }, import);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImport(int id)
    {
        var import = await _context.Imports.FindAsync(id);
        if (import == null)
        {
            return NotFound();
        }

        _context.Imports.Remove(import);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id}/execute")]
    public async Task<IActionResult> ExecuteImport(int id)
    {
        var import = await _context.Imports
            .Include(import => import.Transactions)
            .FirstOrDefaultAsync(import => import.Id == id);

        if (import == null)
        {
            return NotFound();
        }

        if (import.IsExecuted)
        {
            return BadRequest("Import has been executed already.");
        }

        import.IsExecuted = true;
        import.ExecutedAt = DateTimeOffset.UtcNow;

        // Select only import transactions that have a category.
        var transactions = import.Transactions.Where(importTransaction => importTransaction.CategoryId is not null).Select(importTransaction =>
        {
            return new Transaction()
            {
                Amount = importTransaction.Amount,
                CategoryId = importTransaction.CategoryId,
                CurrencyCode = importTransaction.CurrencyCode,
                Date = importTransaction.Date,
                Details = importTransaction.Details,
                Owner = importTransaction.Owner,
                Reference = importTransaction.Reference,
                Type = importTransaction.Type
            };
        });

        // Add transactions to the database.
        await _context.Transactions.AddRangeAsync(transactions);

        // Save changes.
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ImportExists(int id)
    {
        return _context.Imports.Any(e => e.Id == id);
    }
}
