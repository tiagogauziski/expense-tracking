namespace Expense.Tracking.Api.Domain.Models;

public record ImportTransaction : BaseTransaction
{
    /// <summary>
    /// Unique identifier of the transaction.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets whether the entry is a duplicate of an existing imported transaction.
    /// </summary>
    public bool IsDuplicate { get; set; }

    /// <summary>
    /// Gets or sets whether the entry has been selected to be imported.
    /// </summary>
    public bool IsSelected { get; set; }

    public int ImportId { get; set; }

}
