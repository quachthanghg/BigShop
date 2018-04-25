using AutoMapper;
using BigShop.Common;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace BigShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        // GET: Product
        public ActionResult Category()
        {
            return View();
        }

        public ActionResult ProductDetail(int id)
        {
            var model = _productService.GetSigleById(id);
            var responseData = Mapper.Map<Product, ProductViewModel>(model);

            // related product
            var relatedProducts = _productService.GetRelatedProducts(id, 3);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProducts);

            // Comparable to equivalent products
            var equivalentProducts = _productService.EquivalentProducts(id, 3);
            ViewBag.EquivalentProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(equivalentProducts);

            // moreimages
            List<string> lstImages = new JavaScriptSerializer().Deserialize<List<string>>(responseData.MoreImage);
            ViewBag.MoreImages = lstImages;

            // tag
            var tag = _productService.GetListTagByProductID(id);
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tag);
            return View(responseData);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public ActionResult Sort()
        {
            string path = HttpContext.Request.Url.AbsolutePath;
            string[] url = path.Split('/');
            string alias = url[url.Length - 1];
            var lst = _productService.GetParentID(alias);
            if (lst != null)
            {
                var lstproductCategory = _productCategoryService.GetAllByParentId(lst.ParentID);
                ViewBag.lstCategory = lstproductCategory;
                ViewBag.IsRoot = true;
            }
            else
            {
                ViewBag.lstCategory = new List<ProductCategory>();
                ViewBag.IsRoot = false;
            }
            return PartialView();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public ActionResult CategoryList(string alias, string sort = "", int page = 1)
        {
            int totalRow = 0;
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalPages = 0;
            var model = _productService.GetListProductByCategoryPaging(sort, alias, page, pageSize, out totalRow);
            var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
            totalPages = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = responseData,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };
            return PartialView(paginationSet);
        }

        public ActionResult CategoryDetail(string alias, string sort = "", int page = 1)
        {
            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public ActionResult ListProductByTag(string tagID, int page = 1)
        {
            int totalRow = 0;
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalPages = 0;
            var model = _productService.GetListProductByTag(tagID, page, pageSize, out totalRow);
            var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
            totalPages = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = responseData,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };
            return PartialView(paginationSet);
        }
    }
}