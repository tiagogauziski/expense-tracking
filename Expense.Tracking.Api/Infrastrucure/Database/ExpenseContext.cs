using Expense.Tracking.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Infrastrucure.Database;

public class ExpenseContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Import> Imports { get; set; }
    public DbSet<ImportTransaction> ImportTransactions { get; set; }

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
        CategoryEntityConfiguration(modelBuilder);
        TransactionEntityConfiguration<Transaction>(modelBuilder);
        ImportEntityConfiguration(modelBuilder);
        ImportTransactionEntityConfiguration(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CategoryEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasKey(category => category.Id);

        modelBuilder.Entity<Category>()
            .Property(category => category.Name)
            .HasMaxLength(50)
            .IsRequired(true);

        modelBuilder.Entity<Category>()
            .HasOne(category => category.Parent)
            .WithMany(category => category.Children)
            .HasForeignKey(category => category.ParentId)
            .IsRequired(false);
    }

    private static void ImportEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Import>()
            .HasKey(import => import.Id);

        modelBuilder.Entity<Import>()
            .Property(import => import.Layout)
            .HasMaxLength(50)
            .IsRequired(true);

        modelBuilder.Entity<Import>()
            .Property(import => import.CreatedAt)
            .HasDefaultValue(DateTimeOffset.UtcNow);

        modelBuilder.Entity<Import>()
            .HasMany(import => import.Transactions)
            .WithOne(importTransaction => importTransaction.Import)
            .HasForeignKey(e => e.ImportId)
            .IsRequired();
    }

    private static void ImportTransactionEntityConfiguration(ModelBuilder modelBuilder)
    {
        TransactionEntityConfiguration<ImportTransaction>(modelBuilder);
    }

    private static void TransactionEntityConfiguration<T>(ModelBuilder modelBuilder)
        where T : Transaction
    {
        modelBuilder.Entity<Transaction>()
            .HasKey(transaction => transaction.Id);

        modelBuilder.Entity<Transaction>()
            .Property(transaction => transaction.Type)
            .HasMaxLength(50)
            .IsRequired(true);

        modelBuilder.Entity<Transaction>()
            .HasOne(transaction => transaction.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .IsRequired(false);

        modelBuilder.Entity<Transaction>()
            .Property(transaction => transaction.Details)
            .HasMaxLength(100)
            .IsRequired(true);

        modelBuilder.Entity<Transaction>()
            .Property(transaction => transaction.Owner)
            .HasMaxLength(100)
            .IsRequired(false);

        modelBuilder.Entity<Transaction>()
            .Property(transaction => transaction.CurrencyCode)
            .HasMaxLength(10)
            .IsRequired(false)
            .HasDefaultValue("NZD");
    }
}
