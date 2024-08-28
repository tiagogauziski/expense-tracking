namespace Expense.Tracking.Api.Contracts;

public record SettingsImport
{
    public IFormFile? Categories { get; set; }
    public IFormFile? ImportRules { get; set; }
}
