using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using System.Web.Http;

namespace BigShop.Web.Api
{
    [RoutePrefix("Api/Home")]
    [Authorize]
    public class HomeController : BaseApiController
    {
        private IErrorService _errorService;

        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello ThangQV";
        }
    }
}