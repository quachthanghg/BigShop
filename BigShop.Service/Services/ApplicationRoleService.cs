using BigShop.Common.Exceptions;
using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Service.Services
{
    public interface IApplicationRoleService
    {
        ApplicationRole GetDetail(string id);

        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationRole> GetAll();

        ApplicationRole Add(ApplicationRole appRole);

        void Update(ApplicationRole AppRole);

        void Delete(string id);

        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId);

        //Get list role by group id
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);

        void SaveChanges();
    }
    public class ApplicationRoleService : IApplicationRoleService
    {
        private IApplicationRoleRepository _applicationRoleRepository;
        private IApplicationRoleGroupRepository _applicationRoleGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationRoleService(IApplicationRoleRepository applicationRoleRepository, IApplicationRoleGroupRepository applicationRoleGroupRepository, IUnitOfWork unitOfWork)
        {
            this._applicationRoleRepository = applicationRoleRepository;
            this._applicationRoleGroupRepository = applicationRoleGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (_applicationRoleRepository.CheckContains(x => x.Name == appRole.Name))
            {
                throw new NameDuplicateException("Tên không được trùng");
            }
                
            return _applicationRoleRepository.Add(appRole);
        }

        public bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupID)
        {
            _applicationRoleGroupRepository.DeleteMulti(x => x.GroupID == groupID);
            foreach (var roleGroup in roleGroups)
            {
                _applicationRoleGroupRepository.Add(roleGroup);
            }
            return true;
        }

        public void Delete(string id)
        {
            _applicationRoleRepository.DeleteMulti(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _applicationRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Description.Contains(filter));
            }
            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _applicationRoleRepository.GetAll();
        }

        public ApplicationRole GetDetail(string id)
        {
            return _applicationRoleRepository.GetSignleByCondition(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            return _applicationRoleRepository.GetListRoleByGroupId(groupId);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationRole AppRole)
        {
            if (_applicationRoleRepository.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicateException("Tên không được trùng");
            _applicationRoleRepository.Update(AppRole);
        }
    }
}
