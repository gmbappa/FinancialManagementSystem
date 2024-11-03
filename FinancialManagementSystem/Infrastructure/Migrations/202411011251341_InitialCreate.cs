namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChartOfAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        AccountType = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JournalEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntryDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JournalEntryLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JournalEntryId = c.Int(nullable: false),
                        AccountName = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDebit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JournalEntries", t => t.JournalEntryId, cascadeDelete: true)
                .Index(t => t.JournalEntryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalEntryLines", "JournalEntryId", "dbo.JournalEntries");
            DropIndex("dbo.JournalEntryLines", new[] { "JournalEntryId" });
            DropTable("dbo.JournalEntryLines");
            DropTable("dbo.JournalEntries");
            DropTable("dbo.ChartOfAccounts");
        }
    }
}
