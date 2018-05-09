namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitSPStatistic : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueStatistic",
                x => new
                {
                    fromDate = x.String(),
                    toDate = x.String()
                },
                @"
                    SELECT o.CreatedDate as Date, SUM(od.Price * od.Quantity) as Revenue, SUM((od.Price * od.Quantity)-(p.OriginalPrice * od.Quantity)) as Benefit
                    FROM Orders o
                    INNER JOIN OrderDetails od
                    ON o.ID = od.OrderID
                    INNER JOIN Products p
                    ON od.ProductID = p.ID
                    WHERE o.CreatedDate <= @toDate
                    AND o.CreatedDate >= @fromDate
                    GROUP BY o.CreatedDate
                "
                );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}
