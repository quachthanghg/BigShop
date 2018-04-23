using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "BigShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Accout",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
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
                url: "san-pham/{alias1}/{alias2}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "BigShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Product Detail",
               url: "san-pham/{alias1}/{alias2}/{alias3}/{id}",
               defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
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
