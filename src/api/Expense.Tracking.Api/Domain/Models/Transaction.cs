namespace Expense.Tracking.Api.Domain.Models;

public record Transaction : BaseTransaction
{
    

    public override string ToString()
    {
        return $"Type: {Type}, Details: {Details}, Amount: {Amount}, Date {Date}";
    }
}
