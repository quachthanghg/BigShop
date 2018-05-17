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
    [RoutePrefix("Api/PostCategory")]
    public class PostCategoryController : BaseApiController
    {
        private IErrorService _errorService;
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._errorService = errorService;
            this._postCategoryService = postCategoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _postCategoryService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(query);
                var pagination = new PaginationSet<PostCategoryViewModel>()
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
                var model = _postCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        //[HttpGet]
        //[Route("Search")]
        //public HttpResponseMessage Search(HttpRequestMessage requestMessage, string filter, int page, int pageSize)
        //{
        //    return CreateHttpResponse(requestMessage, () =>
        //    {
        //        int totalRow = 0;
        //        var model = _postCategoryService.Search(filter);
        //        totalRow = model.Count();
        //        var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
        //        var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
        //        var pagination = new PaginationSet<ProductCategoryViewModel>()
        //        {
        //            Items = responseData,
        //            Page = page,
        //            TotalCount = totalRow,
        //            TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
        //        };

        //        HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, pagination);
        //        return response;
        //    });
        //}

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryViewModel)
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
                    var postCategory = new PostCategory();
                    postCategory.UpdatePostCategory(postCategoryViewModel);
                    postCategory.CreatedBy = User.Identity.Name;
                    postCategory.CreatedDate = DateTime.Now;
                    _postCategoryService.Add(postCategory);
                    _postCategoryService.SaveChanges();
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(postCategory);
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
                var model = _postCategoryService.GetSigleById(id);
                var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [HttpPut]
        [Route("Update")]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryViewModel)
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
                    var postCategory = _postCategoryService.GetSigleById(postCategoryViewModel.ID);
                    postCategory.UpdatePostCategory(postCategoryViewModel);
                    postCategory.UpdatedDate = DateTime.Now;
                    postCategory.CreatedBy = User.Identity.Name;
                    postCategory.UpdatedBy = User.Identity.Name;
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.SaveChanges();
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(postCategory);
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
                    var postCategory = _postCategoryService.Delete(id);
                    _postCategoryService.SaveChanges();
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(postCategory);
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
                    var listPostCategory = new JavaScriptSerializer().Deserialize<List<int>>(isCheckID);
                    foreach (var item in listPostCategory)
                    {
                        _postCategoryService.Delete(item);
                    }
                    _postCategoryService.SaveChanges();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listPostCategory.Count);
                }
                return responseMessage;
            });
        }
    }
}