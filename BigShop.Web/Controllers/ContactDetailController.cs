using AutoMapper;
using BigShop.Common;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Models;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using BigShop.Web.Infrastructure.Extension;

namespace BigShop.Web.Controllers
{
    public class ContactDetailController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactDetailController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            _contactDetailService = contactDetailService;
            _feedbackService = feedbackService;
        }

        public ActionResult Index()
        {
            var contactDetail = GetContactDetail();
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel();
            feedbackViewModel.ContactDetailViewModels = contactDetail;
            return View(feedbackViewModel);
        }

        [HttpPost]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
       {
            if (this.IsCaptchaValid("Sai mã captch !!! Vui lòng nhập lại"))
            {
                if (ModelState.IsValid)
                {
                    Feedback feedback = new Feedback();
                    feedback.UpdateFeedback(feedbackViewModel);
                    _feedbackService.Create(feedback);
                    _feedbackService.SaveChanges();

                    ViewData["Notify"] = "Gửi phản hồi thành công.";

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/ContactTemplate.html"));
                    content = content.Replace("{{Name}}", feedbackViewModel.Name);
                    content = content.Replace("{{Email}}", feedbackViewModel.Email);
                    content = content.Replace("{{Message}}", feedbackViewModel.Message);

                    var adminEmail = ConfigHelper.GetByKey("AdminEmail");

                    MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ Website", content);
                }
            }
            feedbackViewModel.ContactDetailViewModels = GetContactDetail();
            return View("Index", feedbackViewModel);
        }

        private ContactDetailViewModel GetContactDetail()
        {
            var model = _contactDetailService.GetContact();
            var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return responseData;
        }
    }
}