using CsvHelper;
using Expense.Tracking.Api.Contracts;
using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense.Tracking.Api.Controllers.Api
{
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public SettingsController(ExpenseContext context)
        {
            _context = context;
        }

        [HttpGet("export/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Category
                .Select(category => new CategoryCsv() { Name = category.Name })
                .ToListAsync();

            var stream = new MemoryStream();
            using (var writeFile = new StreamWriter(stream, leaveOpen: true))
            {
                var csv = new CsvWriter(writeFile, CultureInfo.InvariantCulture);
                csv.WriteRecords(categories);
            }

            if (stream == null)
            {
                return NotFound();
            }

            return GenerateCsvFile(categories, $"category-export-{DateTime.Now:yyyyMMdd}.csv"); 
        }

        [HttpGet("export/import-rules")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImportRules()
        {
            var importRules = await _context.ImportRule
                .Select(importRule => new ImportRuleCsvcs()
                {
                    Name = importRule.Name,
                    Condition = importRule.Condition,
                    Category = importRule.Category.Name
                })
                .ToListAsync();

            return GenerateCsvFile(importRules, $"import-rule-export-{DateTime.Now:yyyyMMdd}.csv");
        }

        [HttpDelete("import/delete-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategories()
        {
            var categories = await _context.Category.ToListAsync();

            _context.RemoveRange(categories);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("import/delete-import-rules")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteImportRules()
        {
            var importRules = await _context.ImportRule.ToListAsync();

            _context.RemoveRange(importRules);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private FileStreamResult GenerateCsvFile<T>(IEnumerable<T> data, string fileName)
        {
            var stream = new MemoryStream();
            using (var writeFile = new StreamWriter(stream, leaveOpen: true))
            {
                var csv = new CsvWriter(writeFile, CultureInfo.InvariantCulture);
                csv.WriteRecords(data);
            }
            stream.Position = 0;

            return File(stream, "application/octet-stream", fileName);
        }
    }
}
