using BigShop.Common;
using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;

namespace BigShop.Service.Services
{
    public interface IFooterService
    {
        Footer Add(Footer footer);

        void Update(Footer footer);

        Footer Delete(int id);

        Footer GetSigleById(int id);

        Footer GetAll();

        void Commit();
    }

    public class FooterService : IFooterService
    {
        private IErrorService _errorService;
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;

        public FooterService(IErrorService errorService, IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Footer Add(Footer footer)
        {
            return _footerRepository.Add(footer);
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public Footer Delete(int id)
        {
            return _footerRepository.Delete(id);
        }

        public Footer GetAll()
        {
            return _footerRepository.GetSignleByCondition(x => x.ID == CommonConstants.defaultFooterID);
        }

        public Footer GetSigleById(int id)
        {
            return _footerRepository.GetSignleById(id);
        }

        public void Update(Footer footer)
        {
            _footerRepository.Update(footer);
        }
    }
}