namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldImageHome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageHome", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageHome");
        }
    }
}
