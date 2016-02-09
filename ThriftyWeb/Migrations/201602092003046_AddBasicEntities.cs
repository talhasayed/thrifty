namespace ThriftyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountName = c.String(),
                        AccountCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionLegs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Timestamp = c.DateTime(nullable: false),
                        TransactionLegType = c.Int(nullable: false),
                        Account_Id = c.Guid(),
                        Transaction_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Transactions", t => t.Transaction_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Transaction_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionLegs", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.TransactionLegs", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.TransactionLegs", new[] { "Transaction_Id" });
            DropIndex("dbo.TransactionLegs", new[] { "Account_Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionLegs");
            DropTable("dbo.Accounts");
        }
    }
}
