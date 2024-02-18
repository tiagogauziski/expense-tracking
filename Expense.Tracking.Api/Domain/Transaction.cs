namespace Expense.Tracking.Api.Domain;

public class Transaction
{
    /// <summary>
    /// Unique identifier of the transaction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the transaction. Examples: Purchase, Automatic Payment, Direct Debit, Credit Card
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the category where the expense is located.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the details of the transaction. Helpful to indetify where the transaction was performed.
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    /// Gets or sets the amount of the expense.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets when the transaction was made.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the owner of the purchase.
    /// </summary>
    public string Owner { get; set; }

    /// <summary>
    /// Gets or sets the currency code. Defaults to NZD.
    /// </summary>
    public string CurrencyCode { get; set; }

    public override string ToString()
    {
        return $"Type: {Type}, Details: {Details}, Amount: {Amount}, Date {Date}";
    }
}
