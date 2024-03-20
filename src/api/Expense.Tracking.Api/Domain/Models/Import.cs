namespace Expense.Tracking.Api.Domain.Models;

/// <summary>
/// Represents the import of transactions from a bank statement.
/// </summary>
public record Import
{
    /// <summary>
    /// Unique identifier for the import.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name to identify the import of transaction entries.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The layout used to import the transactions.
    /// </summary>
    public required string Layout { get; set; }

    /// <summary>
    /// The date and time the import was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// The transactions that were imported.
    /// </summary>
    public ICollection<ImportTransaction>? Transactions { get; set; }
}
