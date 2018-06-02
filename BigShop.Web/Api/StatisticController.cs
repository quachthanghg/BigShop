using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigShop.Common.ViewModels;
using System.Web;
using System.Collections.Generic;

namespace BigShop.Web.Api
{
    [RoutePrefix("Api/Statistic")]
    public class StatisticController : BaseApiController
    {
        private IErrorService _errorService;
        private IStatisticService _statisticService;

        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _errorService = errorService;
            _statisticService = statisticService;
        }

        [Route("GetRevenueStatistic")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage requestMessage, string fromDate, string toDate)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, model);
                return responseMessage;
            });
        }

        [Route("TopSaleProduct")]
        [HttpGet]
        public HttpResponseMessage TopSaleProduct(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _statisticService.GetTopSale();
     
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, model);
                return responseMessage;
            });
        }

        [Route("TopSaleProduct")]
        [HttpGet]
        public HttpResponseMessage CountAll(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                //int x = HttpApplication.["Online"];
                //CommonViewModel model = new CommonViewModel()
                //{
                //    PageView = x,
                //};

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK);
                return responseMessage;
            });
        }
    }
}