using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;

namespace BigShop.Service.Services
{
    public interface IPageService
    {
        Page Add(Page page);

        void Update(Page page);

        Page Delete(int id);

        Page GetSigleById(int id);

        IEnumerable<Page> GetAll();

        Page GetByAlias(string alias);
        void Commit();
    }

    public class PageService : IPageService
    {
        private IErrorService _errorService;
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageService(IErrorService errorService, IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public Page Add(Page page)
        {
            return _pageRepository.Add(page);
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public Page Delete(int id)
        {
            return _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSignleByCondition(x => x.Alias == alias);
        }

        public Page GetSigleById(int id)
        {
            return _pageRepository.GetSignleById(id);
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
    }
}