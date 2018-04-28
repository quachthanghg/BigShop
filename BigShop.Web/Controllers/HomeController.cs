using AutoMapper;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;

namespace BigShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        private IFooterService _footerService;
        private ISlideService _slideService;

        public HomeController(IProductService productService, IProductCategoryService productCategoryService, IFooterService footerService, ISlideService slideService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _footerService = footerService;
            _slideService = slideService;
        }

        //[OutputCache(Duration = 3600, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var slide = _slideService.GetAll();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slide);
            var lastestProducts = _productService.GetLastest(4);
            var lastestProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProducts);
            var topProducts = _productService.GetHotProduct(4);
            var topProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topProducts);

            var about = _productService.GetHotProduct(3);
            ViewBag.Top = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(about);

            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Slides = slideViewModel,
                LastestProducts = lastestProductsViewModel,
                TopProducts = topProductsViewModel
            };
            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public ActionResult Footer()
        {
            var footer = _footerService.GetAll();
            var responseData = Mapper.Map<Footer, FooterViewModel>(footer);
            ViewBag.Time = DateTime.Now.ToString("T");
            return PartialView(footer);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public ActionResult Logo()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            var model = _productCategoryService.GetAll();
            var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(responseData);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}