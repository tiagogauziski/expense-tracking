namespace Expense.Tracking.Api.Domain.Models
{
    public abstract record BaseTransaction
    {
        /// <summary>
        /// Gets or sets the type of the transaction. Examples: Purchase, Automatic Payment, Direct Debit, Credit Card
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the category where the expense is located.
        /// </summary>
        public Category? Category { get; set; }

        public Guid? CategoryId { get; set; }

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
        public string? Owner { get; set; }

        /// <summary>
        /// Gets or sets the currency code. Defaults to NZD.
        /// </summary>
        public string? CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the reference for the entry. Can be used to make sure we don't import duplicate transactions.
        /// </summary>
        public string? Reference { get; set; }
    }
}
