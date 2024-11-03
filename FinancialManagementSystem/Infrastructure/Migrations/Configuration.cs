namespace Infrastructure.Migrations
{
    using Core.Entities;
    using Infrastructure.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Data.FinancialDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }      
        protected override void Seed(Infrastructure.Data.FinancialDbContext context)
        {
            // Seeding ChartOfAccount
            context.ChartOfAccounts.AddOrUpdate(
                x => x.Id,
                new ChartOfAccount { Id = 1, AccountName = "Cash", AccountType = "Asset", IsActive = true },
                new ChartOfAccount { Id = 2, AccountName = "Accounts Receivable", AccountType = "Asset", IsActive = true },
                new ChartOfAccount { Id = 3, AccountName = "Inventory", AccountType = "Asset", IsActive = true },
                new ChartOfAccount { Id = 4, AccountName = "Prepaid Expenses", AccountType = "Asset", IsActive = true },
                new ChartOfAccount { Id = 5, AccountName = "Property, Plant & Equipment", AccountType = "Asset", IsActive = true },
                new ChartOfAccount { Id = 6, AccountName = "Accounts Payable", AccountType = "Liability", IsActive = true },
                new ChartOfAccount { Id = 7, AccountName = "Accrued Liabilities", AccountType = "Liability", IsActive = true },
                new ChartOfAccount { Id = 8, AccountName = "Notes Payable", AccountType = "Liability", IsActive = true },
                new ChartOfAccount { Id = 9, AccountName = "Long-term Debt", AccountType = "Liability", IsActive = true },
                new ChartOfAccount { Id = 10, AccountName = "Common Stock", AccountType = "Equity", IsActive = true },
                new ChartOfAccount { Id = 11, AccountName = "Retained Earnings", AccountType = "Equity", IsActive = true },
                new ChartOfAccount { Id = 12, AccountName = "Sales Revenue", AccountType = "Revenue", IsActive = true },
                new ChartOfAccount { Id = 13, AccountName = "Service Revenue", AccountType = "Revenue", IsActive = true },
                new ChartOfAccount { Id = 14, AccountName = "Cost of Goods Sold", AccountType = "Expense", IsActive = true },
                new ChartOfAccount { Id = 15, AccountName = "Salaries Expense", AccountType = "Expense", IsActive = true },
                new ChartOfAccount { Id = 16, AccountName = "Rent Expense", AccountType = "Expense", IsActive = true },
                new ChartOfAccount { Id = 17, AccountName = "Utilities Expense", AccountType = "Expense", IsActive = true },
                new ChartOfAccount { Id = 18, AccountName = "Advertising Expense", AccountType = "Expense", IsActive = true },
                new ChartOfAccount { Id = 19, AccountName = "Interest Expense", AccountType = "Expense", IsActive = true },
                new ChartOfAccount { Id = 20, AccountName = "Income Tax Expense", AccountType = "Expense", IsActive = true }
            );

            // Seeding JournalEntry
            context.JournalEntries.AddOrUpdate(
                x => x.Id,
                new JournalEntry { Id = 1, EntryDate = DateTime.Now, Description = "Initial Entry" },
                new JournalEntry { Id = 2, EntryDate = DateTime.Now.AddDays(-1), Description = "Daily Transactions" },
                new JournalEntry { Id = 3, EntryDate = DateTime.Now.AddDays(-2), Description = "Weekly Summary" },
                new JournalEntry { Id = 4, EntryDate = DateTime.Now.AddDays(-3), Description = "Monthly Review" },
                new JournalEntry { Id = 5, EntryDate = DateTime.Now.AddDays(-4), Description = "Year-End Closing" },
                new JournalEntry { Id = 6, EntryDate = DateTime.Now.AddDays(-5), Description = "Purchase of Supplies" },
                new JournalEntry { Id = 7, EntryDate = DateTime.Now.AddDays(-6), Description = "Payment of Rent" },
                new JournalEntry { Id = 8, EntryDate = DateTime.Now.AddDays(-7), Description = "Sale of Goods" },
                new JournalEntry { Id = 9, EntryDate = DateTime.Now.AddDays(-8), Description = "Utilities Payment" },
                new JournalEntry { Id = 10, EntryDate = DateTime.Now.AddDays(-9), Description = "Advertising Expense" },
                new JournalEntry { Id = 11, EntryDate = DateTime.Now.AddDays(-10), Description = "Service Revenue" },
                new JournalEntry { Id = 12, EntryDate = DateTime.Now.AddDays(-11), Description = "Interest Expense" },
                new JournalEntry { Id = 13, EntryDate = DateTime.Now.AddDays(-12), Description = "Tax Payment" },
                new JournalEntry { Id = 14, EntryDate = DateTime.Now.AddDays(-13), Description = "Insurance Expense" },
                new JournalEntry { Id = 15, EntryDate = DateTime.Now.AddDays(-14), Description = "Salary Payment" },
                new JournalEntry { Id = 16, EntryDate = DateTime.Now.AddDays(-15), Description = "Equipment Purchase" },
                new JournalEntry { Id = 17, EntryDate = DateTime.Now.AddDays(-16), Description = "Repair and Maintenance" },
                new JournalEntry { Id = 18, EntryDate = DateTime.Now.AddDays(-17), Description = "Loan Repayment" },
                new JournalEntry { Id = 19, EntryDate = DateTime.Now.AddDays(-18), Description = "Dividend Payment" },
                new JournalEntry { Id = 20, EntryDate = DateTime.Now.AddDays(-19), Description = "Miscellaneous Expense" }
            );

            // Seeding JournalEntryLines
            context.JournalEntryLines.AddOrUpdate(
                x => x.Id,
                new JournalEntryLine { Id = 1, JournalEntryId = 1, AccountName = "Cash", Amount = 1000, IsDebit = true },
                new JournalEntryLine { Id = 2, JournalEntryId = 1, AccountName = "Sales Revenue", Amount = 1000, IsDebit = false },
                new JournalEntryLine { Id = 3, JournalEntryId = 2, AccountName = "Accounts Receivable", Amount = 500, IsDebit = true },
                new JournalEntryLine { Id = 4, JournalEntryId = 2, AccountName = "Sales Revenue", Amount = 500, IsDebit = false },
                new JournalEntryLine { Id = 5, JournalEntryId = 3, AccountName = "Inventory", Amount = 200, IsDebit = true },
                new JournalEntryLine { Id = 6, JournalEntryId = 3, AccountName = "Accounts Payable", Amount = 200, IsDebit = false },
                new JournalEntryLine { Id = 7, JournalEntryId = 4, AccountName = "Utilities Expense", Amount = 150, IsDebit = true },
                new JournalEntryLine { Id = 8, JournalEntryId = 4, AccountName = "Cash", Amount = 150, IsDebit = false },
                new JournalEntryLine { Id = 9, JournalEntryId = 5, AccountName = "Salaries Expense", Amount = 3000, IsDebit = true },
                new JournalEntryLine { Id = 10, JournalEntryId = 5, AccountName = "Cash", Amount = 3000, IsDebit = false },
                new JournalEntryLine { Id = 11, JournalEntryId = 6, AccountName = "Office Supplies", Amount = 500, IsDebit = true },
                new JournalEntryLine { Id = 12, JournalEntryId = 6, AccountName = "Cash", Amount = 500, IsDebit = false },
                new JournalEntryLine { Id = 13, JournalEntryId = 7, AccountName = "Rent Expense", Amount = 1200, IsDebit = true },
                new JournalEntryLine { Id = 14, JournalEntryId = 7, AccountName = "Cash", Amount = 1200, IsDebit = false },
                new JournalEntryLine { Id = 15, JournalEntryId = 8, AccountName = "Sales Revenue", Amount = 800, IsDebit = false },
                new JournalEntryLine { Id = 16, JournalEntryId = 8, AccountName = "Cash", Amount = 800, IsDebit = true },
                new JournalEntryLine { Id = 17, JournalEntryId = 9, AccountName = "Utilities Payable", Amount = 100, IsDebit = true },
                new JournalEntryLine { Id = 18, JournalEntryId = 9, AccountName = "Cash", Amount = 100, IsDebit = false },
                new JournalEntryLine { Id = 19, JournalEntryId = 10, AccountName = "Advertising Expense", Amount = 250, IsDebit = true },
                new JournalEntryLine { Id = 20, JournalEntryId = 10, AccountName = "Accounts Payable", Amount = 250, IsDebit = false }
            );

            // Save changes
            context.SaveChanges();
        }

    }
}
