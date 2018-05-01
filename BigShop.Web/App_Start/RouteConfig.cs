using System.Web.Mvc;
using System.Web.Routing;

namespace BigShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "About",
                url: "trang/{alias}",
                defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "BigShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Accout",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "BigShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "BigShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "ContactDetail", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "BigShop.Web.Controllers" }
           );

            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "BigShop.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{alias}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "BigShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "san-pham/{category}/{alias}",
                defaults: new { controller = "Product", action = "CategoryDetail", id = UrlParameter.Optional },
                namespaces: new string[] { "BigShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Product Detail",
               url: "san-pham/{alias1}/{alias2}/{id}",
               defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
               namespaces: new string[] { "BigShop.Web.Controllers" }
           );

            routes.MapRoute(
               name: "Tag List",
               url: "tag/{tagID}",
               defaults: new { controller = "Product", action = "ListProductByTag", tagID = UrlParameter.Optional },
               namespaces: new string[] { "BigShop.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}