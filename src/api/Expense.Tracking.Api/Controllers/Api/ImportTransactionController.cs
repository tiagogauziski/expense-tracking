using Expense.Tracking.Api.Domain.Models;
using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Controllers.Api;

[Route("api/import/{importId}/transaction")]
[ApiController]
public class ImportTransactionController : ControllerBase
{
    private readonly ExpenseContext _context;

    public ImportTransactionController(ExpenseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ImportTransaction>>> GetImportTransactions(int importId)
    {
        return await _context.ImportTransactions
            .Where(transactions => transactions.ImportId == importId)
            .ToListAsync();
    }

    // GET: api/ImportTransactions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ImportTransaction>> GetImportTransaction(int importId, int id)
    {
        var importTransaction = await _context.ImportTransactions
            .Where(transactions => transactions.ImportId == importId && transactions.Id == id)
            .FirstOrDefaultAsync();

        if (importTransaction == null)
        {
            return NotFound();
        }

        return importTransaction;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutImportTransaction(int importId, int id, ImportTransaction importTransaction)
    {
        if (id != importTransaction.Id)
        {
            return BadRequest();
        }

        if (importTransaction.ImportId != importId)
        {
            return BadRequest();
        }

        if (!ImportExists(importId))
        {
            return NotFound();
        }

        _context.Entry(importTransaction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ImportTransactionExists(id))
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

    [HttpPost]
    public async Task<ActionResult<ImportTransaction>> PostImportTransaction(int importId, ImportTransaction importTransaction)
    {
        if (!ImportExists(importId))
        {
            return NotFound();
        }

        if (importTransaction.ImportId != importId)
        {
            return BadRequest();
        }

        _context.ImportTransactions.Add(importTransaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetImportTransaction", new { importId, id = importTransaction.Id }, importTransaction);
    }

    // DELETE: api/ImportTransactions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImportTransaction(int importId, int id)
    {
        if (!ImportExists(importId))
        {
            return NotFound();
        }

        var importTransaction = await _context.ImportTransactions
            .Where(transactions => transactions.ImportId == importId && transactions.Id == id)
            .FirstOrDefaultAsync();
        if (importTransaction == null)
        {
            return NotFound();
        }

        _context.ImportTransactions.Remove(importTransaction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ImportTransactionExists(int id)
    {
        return _context.ImportTransactions.Any(e => e.Id == id);
    }

    private bool ImportExists(int importId)
    {
        return _context.Imports.Any(e => e.Id == importId);
    }
}
