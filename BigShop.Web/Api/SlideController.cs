using AutoMapper;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using BigShop.Web.Infrastructure.Extension;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BigShop.Web.Api
{
    [RoutePrefix("Api/Slide")]
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

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, SlideViewModel slideViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var slide = new Slide();
                    slide.UpdateSlide(slideViewModel);
                    _slideService.Add(slide);
                    _slideService.SaveChanges();
                    var responseData = Mapper.Map<Slide, SlideViewModel>(slide);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return responseMessage;
            });
        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        public HttpResponseMessage GetById(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _slideService.GetSigleById(id);
                var responseData = Mapper.Map<Slide, SlideViewModel>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [HttpPut]
        [Route("Update")]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, SlideViewModel slideViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var slide = _slideService.GetSigleById(slideViewModel.ID);
                    slide.UpdateSlide(slideViewModel);
                    _slideService.Update(slide);
                    _slideService.SaveChanges();
                    var responseData = Mapper.Map<Slide, Slide>(slide);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return responseMessage;
            });
        }

        [Route("Delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var slide = _slideService.Delete(id);
                    _slideService.SaveChanges();
                    var responseData = Mapper.Map<Slide, SlideViewModel>(slide);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return responseMessage;
            });
        }

        [Route("DeleteMulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage requestMessage, string isCheckID)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(isCheckID);
                    foreach (var item in listProduct)
                    {
                        _slideService.Delete(item);
                    }
                    _slideService.SaveChanges();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }
                return responseMessage;
            });
        }
    }
}