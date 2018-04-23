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
    [RoutePrefix("Api/ProductCategory")]
    [Authorize]
    public class ProductCategoryController : BaseApiController
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
                var pagination = new PaginationSet<ProductCategoryViewModel>()
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

        [HttpGet]
        [Route("GetAllParents")]
        public HttpResponseMessage GetAllParent(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _productCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [HttpGet]
        [Route("Search")]
        public HttpResponseMessage Search(HttpRequestMessage requestMessage, string filter, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.Search(filter);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
                var pagination = new PaginationSet<ProductCategoryViewModel>()
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
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, ProductCategoryViewModel productCategoryViewModel)
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
                    var productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    productCategory.CreatedBy = User.Identity.Name;
                    productCategory.CreatedDate = DateTime.Now;
                    _productCategoryService.Add(productCategory);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
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
                var model = _productCategoryService.GetSigleById(id);
                var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [HttpPut]
        [Route("Update")]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, ProductCategoryViewModel productCategoryViewModel)
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
                    var productCategory = _productCategoryService.GetSigleById(productCategoryViewModel.ID);
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    productCategory.CreatedBy = User.Identity.Name;
                    productCategory.UpdatedDate = DateTime.Now;
                    _productCategoryService.Update(productCategory);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
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
                    var productCategory = _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
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
                    var listProductCategory = new JavaScriptSerializer().Deserialize<List<int>>(isCheckID);
                    foreach (var item in listProductCategory)
                    {
                        _productCategoryService.Delete(item);
                    }
                    _productCategoryService.SaveChanges();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }
                return responseMessage;
            });
        }
    }
}