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
using BigShop.Web.Infrastructure.NganLuong;

namespace BigShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;
        private ApplicationUserManager _applicationUserManager;
        private IOrderService _orderService;

        private string merchantID = ConfigHelper.GetByKey("MerchantId");
        private string merchantPassword = ConfigHelper.GetByKey("MerchantPassword");
        private string merchantEmail = ConfigHelper.GetByKey("MerchantEmail");

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

        public ActionResult Cart()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            }
            else
            {
                var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
                ViewBag.Quantity = cartSession.Count();
            }
            return PartialView();
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
            var product = _productService.GetSigleById(productID);
            if (cartSession == null)
            {
                cartSession = new List<ShoppingCartViewModel>();
            }
            if (product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "Hết hàng !!!"
                });
            }
            else
            {
                bool isEnough = true;
                if (cartSession.Any(x => x.ProductID == productID))
                {
                    foreach (var item in cartSession)
                    {
                        if (item.ProductID == productID)
                        {
                            isEnough = _productService.SellProduct(item.ProductID, item.Quantity);
                            if (isEnough == true)
                            {
                                item.Quantity += 1;
                                continue;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                }
                else
                {
                    ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel();
                    shoppingCartViewModel.ProductID = productID;
                    shoppingCartViewModel.Product = Mapper.Map<Product, ProductViewModel>(product);
                    shoppingCartViewModel.Quantity = 1;
                    cartSession.Add(shoppingCartViewModel);
                }
                Session[CommonConstants.SessionCart] = cartSession;
                var _quantity = cartSession.Count();
                return Json(new
                {
                    status = true,
                    quantity = _quantity,
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

        public ActionResult CreateOrder(string orderViewModel)
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
            bool isEnough = true;
            foreach (var item in cartSession)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductID = item.ProductID;
                orderDetail.Quantity = item.Quantity;
                orderDetail.Price = item.Product.Price;
                lstOrderDetail.Add(orderDetail);
                isEnough = _productService.SellProduct(item.ProductID, item.Quantity);
                if (isEnough == true)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            if (isEnough)
            {
                var orderReturn = _orderService.Create(orderNew, lstOrderDetail);
                _productService.SaveChanges();
                if (order.PaymentMethod == "CASH")
                { 
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    var currentLink = ConfigHelper.GetByKey("CurrentLink");
                    RequestInfo info = new RequestInfo();
                    info.Merchant_id = merchantID;
                    info.Merchant_password = merchantPassword;
                    info.Receiver_email = merchantEmail;
                    info.cur_code = "vnd";
                    info.bank_code = order.BankCode;

                    info.Order_code = orderReturn.ID.ToString();
                    info.Total_amount = lstOrderDetail.Sum(x => x.Quantity * x.Price).ToString();
                    info.fee_shipping = "0";
                    info.Discount_amount = "0";
                    info.order_description = "Thanh toán đơn hàng tại Bigshop";
                    info.return_url = currentLink + "xac-nhan-don-hang";
                    info.cancel_url = currentLink + "huy-don-hang";

                    info.Buyer_fullname = order.CustomerName;
                    info.Buyer_email = order.CustomerEmail;
                    info.Buyer_mobile = order.CustomerMobile;

                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseInfo result = objNLChecout.GetUrlCheckout(info, order.PaymentMethod);
                    if (result.Error_code == "00")
                    {
                        return Json(new
                        {
                            status = true,
                            urlCheckout = result.Checkout_url,
                            message = result.Description
                        });
                    }
                    else
                        return Json(new
                        {
                            status = false,
                            message = result.Description
                        });
                }
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Số lượng hàng trong kho không đủ"
                });
            }

        }
        public ActionResult OrderConfirm()
        {
            string token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = merchantID;
            info.Merchant_password = merchantPassword;
            info.Token = token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                //update status order
                _orderService.UpdateStatus(int.Parse(result.order_code));
                _orderService.SaveChanges();
                ViewBag.IsSuccess = true;
                ViewBag.Result = "Thanh toán thành công. Chúng tôi sẽ liên hệ lại sớm nhất.";
            }
            else
            {
                ViewBag.IsSuccess = true;
                ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin.";
            }
            return View();
        }
        public ActionResult CancleOrder()
        {
            return View();
        }
    }
}