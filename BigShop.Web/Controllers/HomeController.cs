using AutoMapper;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using System.Linq;

namespace BigShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        private IFooterService _footerService;
        private ISlideService _slideService;
        private IContactDetailService _contactDetailService;
        private IPostService _postService;
        private IPostCategoryService _postCategoryService;

        public HomeController(IProductService productService, IProductCategoryService productCategoryService, IFooterService footerService, ISlideService slideService, IContactDetailService contactDetailService, IPostService postService, IPostCategoryService postCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _footerService = footerService;
            _slideService = slideService;
            _contactDetailService = contactDetailService;
            _postService = postService;
            _postCategoryService = postCategoryService;
        }

        //[OutputCache(Duration = 3600, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var slide = _slideService.GetAll();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slide);
            var lastestProducts = _productService.GetLastest(3);
            var lastestProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProducts);
            var topProducts = _productService.GetHotProduct(3);
            var topProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topProducts);

            var about = _productService.GetHotProduct(3);
            ViewBag.Top = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(about);

            //var topSale = _productService.GetTopSale(3);
            //ViewBag.TopSale = topSale;



            var newPost = _postService.GetAll().OrderByDescending(x=>x.CreatedDate).Take(2);
            ViewBag.Post = newPost;

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

            var productCategory = _productCategoryService.GetAllByParent();
            ViewBag.ProductCategory = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategory);

            var contact = _contactDetailService.GetAll();
            ViewBag.Contact = Mapper.Map<IEnumerable<ContactDetail>, IEnumerable<ContactDetailViewModel>>(contact);

            var postCategory = _postCategoryService.GetAll();
            ViewBag.ProductCategory = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategory>>(postCategory);

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