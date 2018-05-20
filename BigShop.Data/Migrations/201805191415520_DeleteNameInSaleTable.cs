namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteNameInSaleTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sales", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "Name", c => c.String(maxLength: 50));
        }
    }
}
