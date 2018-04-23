using AutoMapper;
using BigShop.Common;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
            var relatedProducts = _productService.GetRelatedProducts(id, 3);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProducts);
            return View(responseData);
        }

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
            }
            return PartialView();
        }
        public ActionResult CategoryList(string alias, string sort = "", int page = 1)
        {
            int totalRow = 0;
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            string path = HttpContext.Request.Url.AbsolutePath;
            string[] url = path.Split('/');

            var lst = _productService.GetParentID(alias);
            if (lst != null)
            {
                var lstproductCategory = _productCategoryService.GetAllByParentId(lst.ParentID);
                ViewBag.lstCategory = lstproductCategory;
            }

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
    }
}