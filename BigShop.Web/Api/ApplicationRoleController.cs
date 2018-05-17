using AutoMapper;
using BigShop.Common.Exceptions;
using BigShop.Model.Models;
using BigShop.Service.Services;
using BigShop.Web.Infrastructure.Core;
using BigShop.Web.Infrastructure.Extension;
using BigShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BigShop.Web.Api
{
    [RoutePrefix("Api/ApplicationRole")]
    [Authorize]
    public class ApplicationRoleController : BaseApiController
    {
        private IApplicationRoleService _applicationRoleService;

        public ApplicationRoleController(IErrorService errorService,
            IApplicationRoleService applicationRoleService) : base(errorService)
        {
            _applicationRoleService = applicationRoleService;
        }

        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _applicationRoleService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<ApplicationRoleViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                PaginationSet<ApplicationRoleViewModel> pagedSet = new PaginationSet<ApplicationRoleViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("GetListAll")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _applicationRoleService.GetAll();
                IEnumerable<ApplicationRoleViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }

        [Route("GetById/{id}")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            ApplicationRole appRole = _applicationRoleService.GetDetail(id);
            if (appRole == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }
            return request.CreateResponse(HttpStatusCode.OK, appRole);
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppRole = new ApplicationRole();
                newAppRole.UpdateApplicationRole(applicationRoleViewModel);
                try
                {
                    _applicationRoleService.Add(newAppRole);
                    _applicationRoleService.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.OK, applicationRoleViewModel);
                }
                catch (NameDuplicateException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var appRole = _applicationRoleService.GetDetail(applicationRoleViewModel.ID);
                try
                {
                    appRole.UpdateApplicationRole(applicationRoleViewModel, "update");
                    _applicationRoleService.Update(appRole);
                    _applicationRoleService.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.OK, appRole);
                }
                catch (NameDuplicateException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            _applicationRoleService.Delete(id);
            _applicationRoleService.SaveChanges();
            return request.CreateResponse(HttpStatusCode.OK, id);
        }

        [Route("DeleteMulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<string>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _applicationRoleService.Delete(item);
                    }

                    _applicationRoleService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}