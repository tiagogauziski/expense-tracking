namespace Expense.Tracking.Api.Contracts
{
    public class CreateImport
    {
        public string Name { get; set; }

        public string Layout { get; set; }

        public IFormFile File { get; set; }
    }
}
