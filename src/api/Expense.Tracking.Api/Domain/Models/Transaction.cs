namespace Expense.Tracking.Api.Domain.Models;

public record Transaction : BaseTransaction
{
    /// <summary>
    /// Unique identifier of the transaction.
    /// </summary>
    public int Id { get; set; }

    public override string ToString()
    {
        return $"Type: {Type}, Details: {Details}, Amount: {Amount}, Date {Date}";
    }
}
