using AutoMapper;
using BigShop.Common.Exceptions;
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
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using static BigShop.Web.App_Start.IdentityConfig;

namespace BigShop.Web.Api
{
    [RoutePrefix("Api/ApplicationGroup")]
    public class ApplicationGroupController : BaseApiController
    {
        private IErrorService _errorService;
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleService _applicationRoleService;
        private ApplicationUserManager _userManager;

        public ApplicationGroupController(IErrorService errorService, IApplicationGroupService applicationGroupService, IApplicationRoleService applicationRoleService, ApplicationUserManager userManager) : base(errorService)
        {
            _errorService = errorService;
            _applicationGroupService = applicationGroupService;
            _applicationRoleService = applicationRoleService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _applicationGroupService.GetAll(page, pageSize, out totalRow, filter);
                totalRow = model.Count();
                var responseData = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);
                var pagination = new PaginationSet<ApplicationGroupViewModel>()
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
        [Route("GetListAll")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _applicationGroupService.GetAll().ToList();
                totalRow = model.Count();
                var responseData = Mapper.Map<ICollection<ApplicationGroup>, ICollection<ApplicationGroupViewModel>>(model);
                var pagination = new PaginationSet<ApplicationGroupViewModel>()
                {
                    Items = responseData,
                    TotalCount = totalRow
                };

                HttpResponseMessage response = requestMessage.CreateResponse(HttpStatusCode.OK, pagination);
                return response;
            });
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppGroup = new ApplicationGroup();
                newAppGroup.Name = appGroupViewModel.Name;
                try
                {
                    var appGroup = _applicationGroupService.Add(newAppGroup);
                    _applicationGroupService.SaveChanges();

                    //save group
                    //var listRoleGroup = new List<ApplicationRoleGroup>();
                    //foreach (var role in appGroupViewModel.ApplicationRoles)
                    //{
                    //    listRoleGroup.Add(new ApplicationRoleGroup()
                    //    {
                    //        GroupID = appGroup.ID,
                    //        RoleID = role.ID
                    //    });
                    //}
                    //_applicationRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                    //_applicationRoleService.SaveChanges();

                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
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

        [Route("GetById/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " is required.");
            }
            ApplicationGroup appGroup = _applicationGroupService.GetDetail(id);
            var appGroupViewModel = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(appGroup);
            if (appGroup == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }
            var listRole = _applicationRoleService.GetListRoleByGroupId(appGroupViewModel.ID);
            appGroupViewModel.ApplicationRoles = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(listRole);
            return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var appGroup = _applicationGroupService.GetDetail(appGroupViewModel.ID);
                try
                {
                    appGroup.UpdateApplicationGroup(appGroupViewModel);
                    _applicationGroupService.Update(appGroup);
                    //_appGroupService.Save();

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in appGroupViewModel.ApplicationRoles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupID = appGroup.ID,
                            RoleID = role.ID
                        });
                    }
                    _applicationRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                    _applicationRoleService.SaveChanges();

                    //add role to user
                    var listRole = _applicationRoleService.GetListRoleByGroupId(appGroup.ID);
                    var listUserInGroup = _applicationGroupService.GetListUserByGroupId(appGroup.ID);
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToArray();
                        foreach (var roleName in listRoleName)
                        {
                            await _userManager.RemoveFromRoleAsync(user.Id, roleName);
                            await _userManager.AddToRoleAsync(user.Id, roleName);
                        }
                    }
                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
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
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = _applicationGroupService.Delete(id);
            _applicationGroupService.SaveChanges();
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
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
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _applicationGroupService.Delete(item);
                    }

                    _applicationGroupService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}