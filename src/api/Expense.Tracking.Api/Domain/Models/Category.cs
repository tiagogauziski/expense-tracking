namespace Expense.Tracking.Api.Domain.Models;

public class Category
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }

    public Guid? ParentId { get; set; }

    public Category? Parent { get; set; }

    public ICollection<Category> Children { get; set; } = new List<Category>();
}


