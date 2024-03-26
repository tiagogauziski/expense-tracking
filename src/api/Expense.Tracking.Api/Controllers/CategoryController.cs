using Expense.Tracking.Api.Domain.Models;
using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ODataController
{
    private readonly ExpenseContext _context;

    public CategoryController(ExpenseContext context)
    {
        _context = context;
    }

    [HttpGet]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IQueryable<Category> GetCategory()
    {
        return _context.Category;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Category.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        if (_context.Category.Any(dbCategory => dbCategory.Name == category.Name && dbCategory.Id != id))
        {
            return Conflict("Category already exists");
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
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

    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        if (_context.Category.Any(dbCategory => dbCategory.Name == category.Name))
        {
            return Conflict("Category already exists");
        }

        _context.Category.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCategory", new { id = category.Id }, category);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Category.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Category.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Category.Any(e => e.Id == id);
    }
}
