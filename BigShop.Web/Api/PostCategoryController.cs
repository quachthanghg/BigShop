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
    [RoutePrefix("Api/PostCategory")]
    [Authorize]
    public class PostCategoryController : BaseApiController
    {
        private IErrorService _errorService;
        private IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._errorService = errorService;
            this._postCategoryService = postCategoryService;
        }
        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                //int totalRow = 0;
                var query = _postCategoryService.GetAll();
                var listProductViewModel = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(query);
                //totalRow = list.Count();
                //var query = list.OrderByDescending(p => p.CreatedDate).Skip(page * pageSize).Take(pageSize);

                //var listProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                //var paginationSet = new PaginationSet<ProductViewModel>()
                //{
                //    Item = listProductViewModel,
                //    Page = page,
                //    TotalCount = totalRow,
                //    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                //};

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProductViewModel);
                return responseMessage;
            });
        }
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategory postCategory)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                _postCategoryService.Add(postCategory);
                _postCategoryService.SaveChanges();
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK);
                return responseMessage;
            });
        }
    }
}