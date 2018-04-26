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
    public class SlideController : BaseApiController
    {
        private IErrorService _errorService;
        private ISlideService _slideService;

        public SlideController(IErrorService errorService, ISlideService slideService) : base(errorService)
        {
            this._errorService = errorService;
            this._slideService = slideService;
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _slideService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(query);
                var pagination = new PaginationSet<SlideViewModel>()
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
        
        //[HttpPost]
        //[Route("Create")]
        //[AllowAnonymous]
        //public HttpResponseMessage Post(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
        //{
        //    return CreateHttpResponse(requestMessage, () =>
        //    {
        //        HttpResponseMessage responseMessage;
        //        if (!ModelState.IsValid)
        //        {
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var slide = new Slide();
        //            slide.UpdateProduct(productViewModel);
        //            slide.CreatedBy = User.Identity.Name;
        //            slide.CreatedDate = DateTime.Now;
        //            _slideService.Add(slide);
        //            _slideService.SaveChanges();
        //            var responseData = Mapper.Map<Product, ProductViewModel>(product);
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
        //        }
        //        return responseMessage;
        //    });
        //}

        //[HttpGet]
        //[Route("GetById/{id:int}")]
        //public HttpResponseMessage GetById(HttpRequestMessage requestMessage, int id)
        //{
        //    return CreateHttpResponse(requestMessage, () =>
        //    {
        //        var model = _productService.GetSigleById(id);
        //        var responseData = Mapper.Map<Product, ProductViewModel>(model);
        //        HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}

        //[HttpPut]
        //[Route("Update")]
        //[AllowAnonymous]
        //public HttpResponseMessage Put(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
        //{
        //    return CreateHttpResponse(requestMessage, () =>
        //    {
        //        HttpResponseMessage responseMessage;
        //        if (!ModelState.IsValid)
        //        {
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var product = _productService.GetSigleById(productViewModel.ID);
        //            product.UpdateProduct(productViewModel);
        //            product.UpdatedDate = DateTime.Now;
        //            product.CreatedBy = User.Identity.Name;
        //            _productService.Update(product);
        //            _productService.SaveChanges();
        //            var responseData = Mapper.Map<Product, ProductViewModel>(product);
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
        //        }
        //        return responseMessage;
        //    });
        //}

        //[Route("Delete")]
        //[HttpDelete]
        //[AllowAnonymous]
        //public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        //{
        //    return CreateHttpResponse(requestMessage, () =>
        //    {
        //        HttpResponseMessage responseMessage = null;
        //        if (!ModelState.IsValid)
        //        {
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var product = _productService.Delete(id);
        //            _productService.SaveChanges();
        //            var responseData = Mapper.Map<Product, ProductViewModel>(product);
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
        //        }
        //        return responseMessage;
        //    });
        //}

        //[Route("DeleteMulti")]
        //[HttpDelete]
        //[AllowAnonymous]
        //public HttpResponseMessage DeleteMulti(HttpRequestMessage requestMessage, string isCheckID)
        //{
        //    return CreateHttpResponse(requestMessage, () =>
        //    {
        //        HttpResponseMessage responseMessage = null;
        //        if (!ModelState.IsValid)
        //        {
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(isCheckID);
        //            foreach (var item in listProduct)
        //            {
        //                _productService.Delete(item);
        //            }
        //            _productService.SaveChanges();
        //            responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProduct.Count);
        //        }
        //        return responseMessage;
        //    });
        //}
    }
}
