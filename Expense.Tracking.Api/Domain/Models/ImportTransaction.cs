namespace Expense.Tracking.Api.Domain.Models;

public record ImportTransaction : BaseTransaction
{
    /// <summary>
    /// Unique identifier of the transaction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets whether the entry is a duplicate of an existing imported transaction.
    /// </summary>
    public bool IsDuplicate { get; set; }

    public Guid ImportId { get; set; }

}
