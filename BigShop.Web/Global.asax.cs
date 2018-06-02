using BigShop.Web.Mappings;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BigShop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfiguration.Config();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Application["PageView"] = 0;
            //Application["Online"] = 0;
        }
        //// Số lượt truy cập
        //protected void Session_Start()
        //{
        //    Application.Lock(); // dùng để đồng bộ hóa
        //    Application["PageView"] = (int)Application["SoLuotTruyCap"] + 1;
        //    Application["Online"] = (int)Application["Online"] + 1;
        //    Application.UnLock();
        //}
        //// Số người đang onlne
        //protected void Session_End()
        //{
        //    Application.Lock();
        //    Application["Online"] = (int)Application["Online"] - 1;
        //    Application.UnLock();
        //}
    }
}