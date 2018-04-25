using AutoMapper;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigShop.Web.Controllers
{
    public class PageController : Controller
    {
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var model = _pageService.GetByAlias(alias);
                var responseData = Mapper.Map<Page, PageViewModel>(model);
                return View(responseData);
            }
        }
    }
}