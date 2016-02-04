namespace ThriftyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountcategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
