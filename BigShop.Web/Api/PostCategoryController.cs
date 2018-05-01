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
    }
}