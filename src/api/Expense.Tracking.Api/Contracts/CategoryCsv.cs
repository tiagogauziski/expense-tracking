using CsvHelper.Configuration.Attributes;

namespace Expense.Tracking.Api.Contracts
{
    public record CategoryCsv
    {
        [Name("name")]
        public required string Name { get; set; }
    }
}
