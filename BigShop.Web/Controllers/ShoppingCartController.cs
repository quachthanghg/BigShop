using AutoMapper;
using BigShop.Common;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BigShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;

        public ShoppingCartController(IProductService productService)
        {
            _productService = productService;
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
    }
}