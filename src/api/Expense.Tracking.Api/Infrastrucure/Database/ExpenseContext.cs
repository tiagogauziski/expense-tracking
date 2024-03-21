using Expense.Tracking.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Infrastrucure.Database;

public class ExpenseContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Import> Imports { get; set; }
    public DbSet<ImportTransaction> ImportTransactions { get; set; }
    public DbSet<Category> Category { get; set; }


    public string DbPath { get; }

    public ExpenseContext(DbContextOptions<ExpenseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CategoryEntityConfiguration(modelBuilder);
        TransactionEntityConfiguration(modelBuilder);
        ImportEntityConfiguration(modelBuilder);
        ImportRuleEntityConfiguration(modelBuilder);
        ImportTransactionEntityConfiguration(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CategoryEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasKey(category => category.Id);

        modelBuilder.Entity<Category>()
            .HasIndex(category => category.Name)
            .IsUnique();

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
            .WithOne()
            .HasForeignKey(e => e.ImportId)
            .IsRequired();
    }

    private static void ImportRuleEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImportRule>()
            .HasKey(importRule => importRule.Id);

        modelBuilder.Entity<ImportRule>()
            .Property(importRule => importRule.Name)
            .HasMaxLength(50)
            .IsRequired(true);

        modelBuilder.Entity<ImportRule>()
            .Property(importRule => importRule.Condition)
            .HasMaxLength(200)
            .IsRequired(true);

        modelBuilder.Entity<ImportRule>()
            .HasOne(importRule => importRule.Category)
            .WithMany()
            .HasForeignKey(importRule => importRule.CategoryId)
            .IsRequired(true);
    }

    private static void ImportTransactionEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImportTransaction>()
            .HasKey(transaction => transaction.Id);

        modelBuilder.Entity<ImportTransaction>()
            .Property(transaction => transaction.IsDuplicate)
            .IsRequired(true);

        modelBuilder.Entity<ImportTransaction>()
            .Property(transaction => transaction.IsSelected)
            .IsRequired(true);

        BaseTransactionEntityConfiguration<ImportTransaction>(modelBuilder);
    }

    private static void TransactionEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasKey(transaction => transaction.Id);

        BaseTransactionEntityConfiguration<Transaction>(modelBuilder);
    }

    private static void BaseTransactionEntityConfiguration<T>(ModelBuilder modelBuilder)
        where T : BaseTransaction
    {
        modelBuilder.Entity<T>()
            .Property(transaction => transaction.Type)
            .HasMaxLength(50)
            .IsRequired(true);

        modelBuilder.Entity<T>()
            .HasOne(transaction => transaction.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .IsRequired(false);

        modelBuilder.Entity<T>()
            .Property(transaction => transaction.Details)
            .HasMaxLength(100)
            .IsRequired(true);

        modelBuilder.Entity<T>()
            .Property(transaction => transaction.Owner)
            .HasMaxLength(100)
            .IsRequired(false);

        modelBuilder.Entity<T>()
            .Property(transaction => transaction.CurrencyCode)
            .HasMaxLength(10)
            .IsRequired(false)
            .HasDefaultValue("NZD");

        modelBuilder.Entity<T>()
            .Property(transaction => transaction.Reference)
            .HasMaxLength(100)
            .IsRequired(false);
    }

public DbSet<Expense.Tracking.Api.Domain.Models.ImportRule> ImportRule { get; set; } = default!;

}
