using Expense.Tracking.Api.Domain.Models;
using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Controllers
{
    [Route("api/importrule")]
    [ApiController]
    public class ImportRulesController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ImportRulesController(ExpenseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportRule>>> GetImportRule()
        {
            return await _context.ImportRule.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImportRule>> GetImportRule(int id)
        {
            var importRule = await _context.ImportRule.FindAsync(id);

            if (importRule == null)
            {
                return NotFound();
            }

            return importRule;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportRule(int id, ImportRule importRule)
        {
            if (id != importRule.Id)
            {
                return BadRequest();
            }

            _context.Entry(importRule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportRuleExists(id))
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
        public async Task<ActionResult<ImportRule>> PostImportRule(ImportRule importRule)
        {
            _context.ImportRule.Add(importRule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImportRule", new { id = importRule.Id }, importRule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportRule(int id)
        {
            var importRule = await _context.ImportRule.FindAsync(id);
            if (importRule == null)
            {
                return NotFound();
            }

            _context.ImportRule.Remove(importRule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportRuleExists(int id)
        {
            return _context.ImportRule.Any(e => e.Id == id);
        }
    }
}
