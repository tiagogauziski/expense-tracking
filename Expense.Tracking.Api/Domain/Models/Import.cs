namespace Expense.Tracking.Api.Domain.Models;

public class Import
{
    public Guid Id { get; set; }

    public required string Layout { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<ImportTransaction> Transactions { get; set; } = new List<ImportTransaction>();
}
