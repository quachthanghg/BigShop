using AutoMapper;
using BigShop.Common;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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

        public OrderDetailController(IErrorService errorService, IProductService productService, IOrderDetailService orderDetailService) : base(errorService)
        {
            this._errorService = errorService;
            this._productService = productService;
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

        [HttpGet]
        [Route("SendEmail")]
        public HttpResponseMessage SendEmail(HttpRequestMessage requestMessage, string email, OrderDetailViewModel orderDetailViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                decimal totalMoney = orderDetailViewModel.Price * orderDetailViewModel.Quantity;
                //string content = .ReadAllText(Server.MapPath("~/Assets/Client/template/ContactTemplate.html"));
                string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"~/Assets/Client/template/ContactTemplate.html");

                string test = File.ReadAllText(filePath);
                test = test.Replace("{{CustomerName}}", orderDetailViewModel.Order.CustomerName);
                test = test.Replace("{{CustomerEmail}}", orderDetailViewModel.Order.CustomerEmail);
                test = test.Replace("{{CustomerAddress}}", orderDetailViewModel.Order.CustomerAddress);
                test = test.Replace("{{CustomerMobile}}", orderDetailViewModel.Order.CustomerMobile);
                test = test.Replace("{{Name}}", orderDetailViewModel.Product.Name);
                test = test.Replace(char.Parse("{{Price}}"), (char)orderDetailViewModel.Product.Price);
                test = test.Replace(char.Parse("{{Quantity}}"), (char)orderDetailViewModel.Quantity);
                test = test.Replace(char.Parse("{{TotalMoney}}"), (char)totalMoney);

                MailHelper.SendMail(email, "Xác nhận đơn hàng từ website", test);

                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK);
                return response;
            });
        }

        [HttpGet]
        [Route("GetOrderDetailById/{id:int}")]
        public HttpResponseMessage GetOrderDetailById(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _orderDetailService.GetOrderDetailById(id);
                var responseData = Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailViewModel>>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
    }
}
