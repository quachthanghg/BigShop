namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMoreSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("ProductIsPhone",
              null,
              @"
                   select pc.Name, Count(p.ID) AS TotalProduct 
                    from ProductCategories pc 
                    join Products p 
                    on p.CategoryID = pc.ID 
                    where pc.ParentID = 1
                    group by pc.Name
                "
              );
            CreateStoredProcedure("ProductIsTablet",
             null,
             @"
                    select pc.Name, Count(p.ID) AS TotalProduct 
                    from ProductCategories pc 
                    join Products p 
                    on p.CategoryID = pc.ID 
                    where pc.ParentID = 2
                    group by pc.Name
                "
             );
            CreateStoredProcedure("ProductIsLaptop",
             null,
             @"
                    select pc.Name, Count(p.ID) AS TotalProduct 
                    from ProductCategories pc 
                    join Products p 
                    on p.CategoryID = pc.ID 
                    where pc.ParentID = 3
                    group by pc.Name
                "
             );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.ProductIsPhone");
            DropStoredProcedure("dbo.ProductIsTablet");
            DropStoredProcedure("dbo.ProductIsLaptop");
        }
    }
}
