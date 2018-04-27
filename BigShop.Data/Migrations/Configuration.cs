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
            CreateSlideSample(context);
            CreateProductSample(context);
            CreatePageSample(context);
            //CreateContactDetailSample(context);
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

        private void CreateProductSample(BigShopDbContext context)
        {
            if (context.Products.Count() == 0)
            {
                List<Product> list = new List<Product>()
                {
                    new Product(){Name="IPhone 6", Alias="iphone-6", Description="A", CategoryID = 1, Status=true},
                    new Product(){Name="IPhone 7", Alias="iphone-7", Description="A", CategoryID = 2, Status=true},
                    new Product(){Name="IPhone 8", Alias="iphone-8", Description="A", CategoryID = 3, Status=true}
                };
                context.Products.AddRange(list);
                context.SaveChanges();
            }
        }

        private void CreateSlideSample(BigShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> list = new List<Slide>()
                {
                    new Slide(){Name="Slide 1", URL= "#", DisplayOrder = 1, Description="hinh anh 1", Content= "OK",Image="/Assets/Client/images/bnr-1.jpg", Status= true},
                    new Slide(){Name="Slide 2", URL= "#", DisplayOrder = 2, Description="hinh anh 2", Content= "OK",Image="/Assets/Client/images/bnr-2.jpg", Status= true},
                    new Slide(){Name="Slide 3", URL= "#", DisplayOrder = 3, Description="hinh anh 3", Content= "OK",Image="/Assets/Client/images/bnr-3.jpg", Status= true}
                };
                context.Slides.AddRange(list);
                context.SaveChanges();
            }
        }

        private void CreatePageSample(BigShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                List<Page> list = new List<Page>()
                {
                    new Page(){
                        Name ="Giới Thiệu",
                        Alias = "gioi-thieu",
                        Content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?"}
                };
                context.Pages.AddRange(list);
                context.SaveChanges();
            }
        }

        private void CreateContactSample(BigShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {

                var contact = new ContactDetail()
                {
                    ID = 1,
                    Name = "BigShop",
                    Address = "Số 8 Thương Kiều",
                    Email = "quachthanghg@gmail.com",
                    PhoneNumber = "+84 978 850 339",
                    Website = "http://BigShop.com.vn",
                    Lat = 22.2192672,
                    Lng = 104.7988248,
                    Status = true
                };
                context.ContactDetails.Add(contact);
                context.SaveChanges();
            }
        }
    }
}