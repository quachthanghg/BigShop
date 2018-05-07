using BigShop.Common.Exceptions;
using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace BigShop.Service.Services
{
    public interface IApplicationGroupService
    {
        ApplicationGroup Add(ApplicationGroup applicationGroup);

        void Update(ApplicationGroup applicationGroup);

        ApplicationGroup Delete(int id);

        ApplicationGroup GetDetail(int id);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationGroup> GetAll();

        bool AddUserToGroup(IEnumerable<ApplicationUserGroup> group, string userID);

        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userID);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupID);

        void SaveChanges();
    }

    public class ApplicationGroupService : IApplicationGroupService
    {
        private IErrorService _errorService;
        private IApplicationGroupRepository _applicationGroupRepository;
        private IApplicationUserGroupRepository _applicationUserGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationGroupService(IErrorService errorService, IApplicationGroupRepository applicationGroupRepository, IApplicationUserGroupRepository applicationUserGroupRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._applicationGroupRepository = applicationGroupRepository;
            this._applicationUserGroupRepository = applicationUserGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationGroup Add(ApplicationGroup applicationGroup)
        {
            if (_applicationGroupRepository.CheckContains(x => x.Name == applicationGroup.Name))
            {
                throw new NameDuplicateException("Tên không được trùng");
            }
            return _applicationGroupRepository.Add(applicationGroup);
        }

        public bool AddUserToGroup(IEnumerable<ApplicationUserGroup> group, string userID)
        {
            _applicationUserGroupRepository.DeleteMulti(x => x.UserID == userID);
            foreach (var item in group)
            {
                _applicationUserGroupRepository.Add(item);
            }
            return true;
        }

        public ApplicationGroup Delete(int id)
        {
            var appGroup = this._applicationGroupRepository.GetSignleById(id);
            return _applicationGroupRepository.Delete(appGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _applicationGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _applicationGroupRepository.GetAll();
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _applicationGroupRepository.GetSignleById(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userID)
        {
            return _applicationGroupRepository.GetListGroupByUserId(userID);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupID)
        {
            return _applicationGroupRepository.GetListUserByGroupId(groupID);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup applicationGroup)
        {
            if (_applicationGroupRepository.CheckContains(x => x.Name == applicationGroup.Name && x.ID != applicationGroup.ID))
            {
                throw new NameDuplicateException("Tên không được trùng");
            }
            _applicationGroupRepository.Update(applicationGroup);
        }
    }
}