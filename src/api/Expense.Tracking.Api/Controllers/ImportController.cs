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
    public async Task<ActionResult<Import>> GetImport(Guid id)
    {
        var import = await _context.Imports
            .Include(import => import.Transactions)
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
    public async Task<IActionResult> PutImport(Guid id, Import import)
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
    public async Task<ActionResult<Import>> PostImport([FromForm] CreateImport request)
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

        BankCsvLayoutEngine engine = new BankCsvLayoutEngine();
        var transactions = await engine.Execute(request.File.OpenReadStream());
        import.Transactions = transactions.ToList();

        _context.Imports.Add(import);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetImport", new { id = import.Id }, import);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImport(Guid id)
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

    private bool ImportExists(Guid id)
    {
        return _context.Imports.Any(e => e.Id == id);
    }
}
