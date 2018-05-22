using AutoMapper;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigShop.Web.Api
{
    [RoutePrefix("Api/Order")]
    public class OrderController : BaseApiController
    {
        private IErrorService _errorService;

        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;

        public OrderController(IErrorService errorService, IOrderService orderService, IOrderDetailService orderDetailService) : base(errorService)
        {
            _errorService = errorService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        [Route("GetAll")]
        [HttpGet]

        public HttpResponseMessage Get(HttpRequestMessage requestMessage, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query);
                var pagination = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, pagination);
                return response;
            });
        }
    }
}
