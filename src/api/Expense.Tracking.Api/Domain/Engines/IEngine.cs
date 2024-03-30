using Expense.Tracking.Api.Domain.Models;

namespace Expense.Tracking.Api.Domain.Engines
{
    public interface IEngine
    {
        Task<IEnumerable<ImportTransaction>> Execute(Stream stream, CancellationToken cancellationToken);

        IEnumerable<ImportTransaction> ApplyRules(IEnumerable<ImportTransaction> importTransactions);
    }
}
