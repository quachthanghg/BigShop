using BigShop.Common;
using BigShop.Data;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static BigShop.Web.App_Start.IdentityConfig;

[assembly: OwinStartup(typeof(BigShop.Web.App_Start.Startup))]

namespace BigShop.Web.App_Start
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(BigShopDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateManager);

            // tao Owincontext de quan ly usermanager
            // owin lam giam su phu thuoc giua server va application
            app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateManager);
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                AllowInsecureHttp = true,
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/dang-nhap.html"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
            appId: "185965845388898",
               appSecret: "237d39cd19310d9fa42e706e7117b8b6");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "542375550881-0p43uomt2goknrm4q83d07uocijcl3rj.apps.googleusercontent.com",
                ClientSecret = "BaovK6Gn0IBgBR08lOSFz_bd"
            });
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
                ApplicationUser user;
                try
                {
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
                catch
                {
                    // Could not retrieve the user due to error.
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }
                if (user != null)
                {
                    //var applicationGroupService = ServiceFactory.Get<IApplicationGroupService>();
                    //var lstGroup = applicationGroupService.GetListGroupByUserId(user.Id).ToList();
                    //if (lstGroup.Any(x => x.Name == CommonConstants.Administractor))
                    //{
                        ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                                          user,
                                                          DefaultAuthenticationTypes.ExternalBearer);
                        context.Validated(identity);
                    //}
                    //else
                    //{
                    //    context.Rejected();
                    //    context.SetError("Invalid_Group", "Bạn không phải admin, đừng cố gắng làm gì cho mất công");
                    //}

                }
                else
                {
                    context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.");
                    context.Rejected();
                }
            }
        }

        private static UserManager<ApplicationUser> CreateManager(IdentityFactoryOptions<UserManager<ApplicationUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context.Get<BigShopDbContext>());
            var owinManager = new UserManager<ApplicationUser>(userStore);

            return owinManager;
        }
    }
}