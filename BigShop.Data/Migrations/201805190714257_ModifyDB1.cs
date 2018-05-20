namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Policies", "Description", c => c.String());
            AlterColumn("dbo.Sales", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Policies", "Description", c => c.String(maxLength: 250));
        }
    }
}
