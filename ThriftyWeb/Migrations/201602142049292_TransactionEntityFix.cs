namespace ThriftyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionEntityFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "Account_Id" });
            DropColumn("dbo.Transactions", "Account_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Account_Id", c => c.Guid());
            CreateIndex("dbo.Transactions", "Account_Id");
            AddForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
