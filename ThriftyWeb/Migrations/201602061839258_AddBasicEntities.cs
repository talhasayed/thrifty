namespace ThriftyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountCategories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountName = c.String(),
                        AccountCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountCategories", t => t.AccountCategory_Id)
                .Index(t => t.AccountCategory_Id);
            
            CreateTable(
                "dbo.TransactionLegs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Account_Id = c.Guid(),
                        Transaction_Id = c.Guid(),
                        TransactionLegType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Transactions", t => t.Transaction_Id)
                .ForeignKey("dbo.TransactionLegTypes", t => t.TransactionLegType_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Transaction_Id)
                .Index(t => t.TransactionLegType_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionLegTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionLegs", "TransactionLegType_Id", "dbo.TransactionLegTypes");
            DropForeignKey("dbo.TransactionLegs", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.TransactionLegs", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "AccountCategory_Id", "dbo.AccountCategories");
            DropIndex("dbo.TransactionLegs", new[] { "TransactionLegType_Id" });
            DropIndex("dbo.TransactionLegs", new[] { "Transaction_Id" });
            DropIndex("dbo.TransactionLegs", new[] { "Account_Id" });
            DropIndex("dbo.Accounts", new[] { "AccountCategory_Id" });
            DropTable("dbo.TransactionLegTypes");
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionLegs");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountCategories");
        }
    }
}
