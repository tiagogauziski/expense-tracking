using CsvHelper.Configuration.Attributes;

namespace Expense.Tracking.Api.Contracts
{
    public record ImportRuleCsv
    {
        [Name("name")]
        public required string Name { get; set; }

        [Name("condition")]
        public required string Condition { get; set; }

        [Name("category")]
        public required string Category { get; set; }
    }
}
