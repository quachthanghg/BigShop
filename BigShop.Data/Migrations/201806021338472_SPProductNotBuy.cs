namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SPProductNotBuy : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("ProductNotBuy",
               null,
               @"
                    select p.Name AS Name
                    From Products p
                    left outer join OrderDetails od
                    on od.ProductID = p.ID
                    where od.ProductID is null
                "
               );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.ProductNotBuy");
        }
    }
}
