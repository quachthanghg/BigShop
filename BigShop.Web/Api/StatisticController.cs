using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigShop.Common.ViewModels;
using System.Web;
using System.Collections.Generic;
using System.Linq;

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

        [Route("ProductNotBuy")]
        [HttpGet]
        public HttpResponseMessage ProductNotBuy(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _statisticService.GetProductNotBuy();

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, model);
                return responseMessage;
            });
        }

        [Route("ProductIsPhone")]
        [HttpGet]
        public HttpResponseMessage ProductIsPhone(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _statisticService.GetProductIsPhone();

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, model);
                return responseMessage;
            });
        }

        [Route("ProductIsTablet")]
        [HttpGet]
        public HttpResponseMessage ProductIsTablet(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _statisticService.GetProductIsTablet();

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, model);
                return responseMessage;
            });
        }

        [Route("ProductIsLaptop")]
        [HttpGet]
        public HttpResponseMessage ProductIsLaptop(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _statisticService.GetProductIsLaptop();

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, model);
                return responseMessage;
            });
        }
    }
}