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
using System.Net.Http;
using System.Web.Http;
using System.Web.Mail;

namespace BigShop.Web.Api
{
    [RoutePrefix("Api/OrderDetail")]
    public class OrderDetailController : BaseApiController
    {
        private IErrorService _errorService;
        private IProductService _productService;
        private IOrderDetailService _orderDetailService;
        private IOrderService _orderService;

        public OrderDetailController(IErrorService errorService, IProductService productService, IOrderDetailService orderDetailService) : base(errorService)
        {
            this._errorService = errorService;
            this._productService = productService;
            this._orderDetailService = orderDetailService;
            this._orderDetailService = orderDetailService;
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _orderDetailService.GetAllOrderDetail();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Order.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailViewModel >> (query);
                var pagination = new PaginationSet<OrderDetailViewModel>
                {
                    Items = responseData.OrderBy(x => x.OrderID),
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
