namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSPTopSale : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("TopSale",
                null,
                @"
                    select top 3 ProductID AS IdItem, p.Name AS NameItem, sum(od.Quantity) as qua
                    From OrderDetails od
                    join Products p
                    on od.ProductID = p.id
                    join Orders o
                    on o.ID = od.OrderID
                    group by ProductID, p.Name
                    order by qua desc
                "
                );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.TopSale");
        }
    }
}
