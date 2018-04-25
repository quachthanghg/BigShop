namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProfile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Profile", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "Profile", c => c.String(maxLength: 500));
        }
    }
}
