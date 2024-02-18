using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Infrastrucure.Database;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Transaction>()
            .HasKey(transaction => transaction.Id);

        modelBuilder.Entity<Domain.Transaction>()
            .Property(transaction => transaction.Type)
            .HasMaxLength(50)
            .IsRequired(true);

        modelBuilder.Entity<Domain.Transaction>()
            .Property(transaction => transaction.Category)
            .HasMaxLength(100)
            .IsRequired(false);

        modelBuilder.Entity<Domain.Transaction>()
            .Property(transaction => transaction.Details)
            .HasMaxLength(100)
            .IsRequired(true);

        modelBuilder.Entity<Domain.Transaction>()
            .Property(transaction => transaction.Owner)
            .HasMaxLength(100)
            .IsRequired(false);

        modelBuilder.Entity<Domain.Transaction>()
            .Property(transaction => transaction.CurrencyCode)
            .HasMaxLength(10)
            .IsRequired(false)
            .HasDefaultValue("NZD");

        base.OnModelCreating(modelBuilder);
    }
}
