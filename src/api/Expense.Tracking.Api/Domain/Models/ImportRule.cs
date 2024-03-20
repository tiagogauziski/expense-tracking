namespace Expense.Tracking.Api.Domain.Models
{
    public class ImportRule
    {
        /// <summary>
        /// Gets or sets the unique identifier for the import rule.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a friendly name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a condition that will be used to match transactions. The condition will be applied to the Details property of the transaction.
        /// </summary>
        public string DetailsCondition { get; set; }

        /// <summary>
        /// Gets or sets a condition that will be used to match transactions. The condition will be applied to the Type property of the transaction.
        /// </summary>
        public string TypeCondition { get; set; }

        /// <summary>
        /// Gets or sets the category that will be assigned to the transaction.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
