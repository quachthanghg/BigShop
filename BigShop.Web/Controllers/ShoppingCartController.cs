using AutoMapper;
using BigShop.Common;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static BigShop.Web.App_Start.IdentityConfig;
using BigShop.Web.Infrastructure.Extension;

namespace BigShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;
        private ApplicationUserManager _applicationUserManager;
        private IOrderService _orderService;

        public ShoppingCartController(IProductService productService, ApplicationUserManager applicationUserManager, IOrderService orderService)
        {
            _productService = productService;
            _applicationUserManager = applicationUserManager;
            this._orderService = orderService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            }
            return View();
        }

        public JsonResult GetUserLoginInfo()
        {
            if (Request.IsAuthenticated)
            {
                var userID = User.Identity.GetUserId();
                var user = _applicationUserManager.FindById(userID);
                return Json(new
                {
                    status = true,
                    data = user
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public ActionResult CheckOut()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                return Redirect("/gio-hang");
            }
            return View();
        }

        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            }
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            return Json(new
            {
                status = true,
                data = cartSession
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productID)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession == null)
            {
                cartSession = new List<ShoppingCartViewModel>();
            }
            var quantityProduct = _productService.GetSigleById(productID);
            if (quantityProduct.Quantity < 0)
            {
                return Json(new
                {
                    status = false,
                });
            }
            else
            {
                if (cartSession.Any(x => x.ProductID == productID))
                {
                    foreach (var item in cartSession)
                    {
                        if (item.ProductID == productID)
                        {
                            item.Quantity += 1;
                        }
                    }
                }
                else
                {
                    ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel();
                    shoppingCartViewModel.ProductID = productID;
                    var product = _productService.GetSigleById(productID);
                    shoppingCartViewModel.Product = Mapper.Map<Product, ProductViewModel>(product);
                    shoppingCartViewModel.Quantity = 1;
                    cartSession.Add(shoppingCartViewModel);
                }
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true,
                });
            }
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach (var item in cartSession)
            {
                foreach (var itemProduct in cartViewModel)
                {
                    if (item.ProductID == itemProduct.ProductID)
                    {
                        item.Quantity = itemProduct.Quantity;
                    }
                }
            }
            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productID)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                foreach (var item in cartSession)
                {
                    cartSession.RemoveAll(x => x.ProductID == productID);
                    Session[CommonConstants.SessionCart] = cartSession;
                }
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            Order orderNew = new Order();
            orderNew.UpdateOrder(order);
            if (Request.IsAuthenticated)
            {
                orderNew.CustomerID = User.Identity.GetUserId();
                order.CreatedBy = User.Identity.GetUserName();
            }

            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
            foreach (var item in cartSession)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductID = item.ProductID;
                orderDetail.Quantity = item.Quantity;
                lstOrderDetail.Add(orderDetail);
            }
            _orderService.Create(orderNew, lstOrderDetail);
            return Json(new
            {
                status = true
            });
        }
    }
}