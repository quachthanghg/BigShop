using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;

namespace BigShop.Service.Services
{
    public interface IPolicyService
    {
        Policy Add(Policy policy);

        void Update(Policy policy);

        Policy GetSigleById(int id);

        Policy Delete(int id);
        IEnumerable<Policy> GetAll();

        void SaveChanges();
    }

    public class PolicyService : IPolicyService
    {
        private IErrorService _errorService;
        private IPolicyRepository _policyRepository;
        private IUnitOfWork _unitOfWork;

        public PolicyService(IErrorService errorService, IPolicyRepository policyRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._policyRepository = policyRepository;
            this._unitOfWork = unitOfWork;
        }

        public Policy Add(Policy policy)
        {
            return _policyRepository.Add(policy);
        }

        public Policy Delete(int id)
        {
            return _policyRepository.Delete(id);
        }

        public IEnumerable<Policy> GetAll()
        {
            return _policyRepository.GetAll();
        }

        public Policy GetSigleById(int id)
        {
            return _policyRepository.GetSignleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Policy policy)
        {
            _policyRepository.Update(policy);
        }
    }
}