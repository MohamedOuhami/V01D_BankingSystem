using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Relationships
        modelBuilder.Entity<User>()
        .HasMany(u => u.Accounts)
        .WithOne(a => a.User)
        .HasForeignKey(a => a.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        // Account Relationships
        modelBuilder.Entity<Account>()
        .HasMany(a => a.Cards)
        .WithMany(c => c.Accounts)
        .UsingEntity("AccountsCards");

        modelBuilder.Entity<Account>()
        .HasMany(a => a.Operations)
        .WithOne(o => o.Account)
        .HasForeignKey(o => o.AccountId)
        .OnDelete(DeleteBehavior.Restrict);

        // Transaction Relationships
        modelBuilder.Entity<Transaction>()
        .HasOne(t => t.FromAccount)
        .WithMany(a => a.FromTransactions)
        .HasForeignKey(t => t.FromAccountId)
        .OnDelete(DeleteBehavior.Restrict);

         modelBuilder.Entity<Transaction>()
        .HasOne(t => t.ToAccount)
        .WithMany(a => a.ToTransactions)
        .HasForeignKey(t => t.ToAccountId)
        .OnDelete(DeleteBehavior.Restrict);
   
    }

    // Adding the models to the databases
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}