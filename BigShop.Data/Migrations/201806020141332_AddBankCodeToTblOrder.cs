namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankCodeToTblOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "BankCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "BankCode");
        }
    }
}
