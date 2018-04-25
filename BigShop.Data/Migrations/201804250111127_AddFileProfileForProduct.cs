namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileProfileForProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Profile", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Profile");
        }
    }
}
