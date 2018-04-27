using AutoMapper;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Models;
using System.Web.Mvc;

namespace BigShop.Web.Controllers
{
    public class ContactDetailController : Controller
    {
        private IContactDetailService _contactDetailService;

        public ContactDetailController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }

        // GET: ContactDetail
        public ActionResult Index()
        {
            var model = _contactDetailService.GetContact();
            var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(responseData);
        }
    }
}