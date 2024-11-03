using Core.Entities;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class FinancialDbContext : DbContext
    {
        public FinancialDbContext() : base("name=FinancialDbContext") {  
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalEntryLine> JournalEntryLines { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<ChartOfAccount>()
                        .Property(c => c.AccountName)
                        .IsRequired(); 
        }
    }
}
