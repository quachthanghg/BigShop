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
    [RoutePrefix("Api/Post")]
    public class PostController : BaseApiController
    {
        private IErrorService _errorService;
        private IPostService _postService;

        public PostController(IErrorService errorService, IPostService postService) : base(errorService)
        {
            this._errorService = errorService;
            this._postService = postService;
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _postService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(query);
                var pagination = new PaginationSet<PostViewModel>()
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
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostViewModel postViewModel)
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
                    var post = new Post();
                    post.UpdatePost(postViewModel);
                    post.CreatedBy = User.Identity.Name;
                    post.CreatedDate = DateTime.Now;
                    _postService.Add(post);
                    _postService.SaveChanges();
                    var responseData = Mapper.Map<Post, PostViewModel>(post);
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
                var model = _postService.GetSigleById(id);
                var responseData = Mapper.Map<Post, PostViewModel>(model);
                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [HttpPut]
        [Route("Update")]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostViewModel postViewModel)
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
                    var post = _postService.GetSigleById(postViewModel.ID);
                    post.UpdatePost(postViewModel);
                    post.UpdatedDate = DateTime.Now;
                    post.CreatedBy = User.Identity.Name;
                    _postService.Update(post);
                    _postService.SaveChanges();
                    var responseData = Mapper.Map<Post, PostViewModel>(post);
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
                    var post = _postService.Delete(id);
                    _postService.SaveChanges();
                    var responseData = Mapper.Map<Post, PostViewModel>(post);
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
                    var listPost = new JavaScriptSerializer().Deserialize<List<int>>(isCheckID);
                    foreach (var item in listPost)
                    {
                        _postService.Delete(item);
                    }
                    _postService.SaveChanges();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listPost.Count);
                }
                return responseMessage;
            });
        }
    }
}