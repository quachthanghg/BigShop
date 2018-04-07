namespace BigShop.Data.Migrations
{
    using BigShop.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BigShop.Data.BigShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BigShop.Data.BigShopDbContext context)
        {
            CreateAccountSample(context);
            CreateProductCategorySample(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }

        private void CreateAccountSample(BigShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BigShopDbContext()));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new BigShopDbContext()));
                var user = new ApplicationUser()
                {
                    UserName = "quachthanghg",
                    Email = "quachthanghg@gmail.com",
                    EmailConfirmed = true,
                    Birthday = DateTime.Now,
                    FullName = "QuachThang"
                };
                manager.Create(user, "123456!");
                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }
                var adminUser = manager.FindByEmail("quachthanghg@gmail.com");
                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }
        }

        private void CreateProductCategorySample(BigShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> list = new List<ProductCategory>()
                {
                    new ProductCategory(){Name="Điện thoại", Alias="dien-thoai", Status=true},
                    new ProductCategory(){Name="Máy tính bảng", Alias="may-tinh-bang", Status=true},
                    new ProductCategory(){Name="Laptop", Alias="laptop", Status=true}
                };
                context.ProductCategories.AddRange(list);
                context.SaveChanges();
            }
        }
    }
}