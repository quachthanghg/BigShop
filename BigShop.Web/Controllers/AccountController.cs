using BigShop.Common;
using BigShop.Model.Models;
using BigShop.Web.Models;
using CaptchaMvc.HtmlHelpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static BigShop.Web.App_Start.IdentityConfig;

namespace BigShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel, string url)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(loginViewModel.UserName, loginViewModel.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity claimsIdentity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties authenticationProperties = new AuthenticationProperties();
                    authenticationProperties.IsPersistent = loginViewModel.RememberMe;
                    authenticationManager.SignIn(authenticationProperties, claimsIdentity);
                    if (Url.IsLocalUrl(url))
                    {
                        return Redirect(url);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
            }
            ViewBag.Url = url;
            return View(loginViewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (this.IsCaptchaValid("Sai mã captch !!! Vui lòng nhập lại"))
            {
                if (ModelState.IsValid)
                {
                    var email = await _userManager.FindByEmailAsync(registerViewModel.Email);
                    if (email != null)
                    {
                        ModelState.AddModelError("email", "Email đã tồn tại");
                        return View(registerViewModel);
                    }

                    var userName = await _userManager.FindByNameAsync(registerViewModel.UserName);
                    if (userName != null)
                    {
                        ModelState.AddModelError("userName", "Tài khoản đã tồn tại");
                        return View(registerViewModel);
                    }

                    var users = new ApplicationUser()
                    {
                        UserName = registerViewModel.UserName,
                        FullName = registerViewModel.FullName,
                        PasswordHash = registerViewModel.Password,
                        Email = registerViewModel.Email,
                        Address = registerViewModel.Address,
                        PhoneNumber = registerViewModel.PhoneNumber
                    };
                    await _userManager.CreateAsync(users, registerViewModel.Password);

                    var adminUser = await _userManager.FindByEmailAsync(registerViewModel.Email);
                    if (adminUser != null)
                    {
                        await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "User" });
                    }
                    TempData["Success"] = "Đăng ký thành công";

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/NewUserTemplate.html"));
                    content = content.Replace("{{UserName}}", adminUser.FullName);
                    content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink"));

                    MailHelper.SendMail(adminUser.Email, "Đăng ký tài khoản thành công", content);
                }
                return View();
            }
            return View();
        }
    }
}