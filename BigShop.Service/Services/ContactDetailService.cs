using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;

namespace BigShop.Service.Services
{
    public interface IContactDetailService
    {
        ContactDetail GetContact();

        IEnumerable<ContactDetail> GetAll();
    }

    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            return _contactDetailRepository.GetAll();
        }

        public ContactDetail GetContact()
        {
            return _contactDetailRepository.GetSignleByCondition(x => x.Status == true);
        }
    }
}