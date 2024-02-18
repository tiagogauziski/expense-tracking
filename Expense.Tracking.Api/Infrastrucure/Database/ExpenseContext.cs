using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Infrastrucure.Database
{
    public class ExpenseContext : DbContext
    {
        public DbSet<Domain.Transaction> Transactions { get; set; }

        public string DbPath { get; }

        public ExpenseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "expenses.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
