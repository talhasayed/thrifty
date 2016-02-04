namespace ThriftyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountCategories");
        }
    }
}
