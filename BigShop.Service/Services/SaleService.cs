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
    public interface ISaleService
    {
        Sale Add(Sale sale);

        void Update(Sale sale);

        Sale GetSigleById(int id);

        Sale Delete(int id);
        IEnumerable<Sale> GetAll();

        IEnumerable<Sale> GetSaleProduct(int id);

        void SaveChanges();
    }
    public class SaleService : ISaleService
    {
        private IErrorService _errorService;
        private ISaleRepository _saleRepository;
        private IUnitOfWork _unitOfWork;

        public SaleService(IErrorService errorService, ISaleRepository saleRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._saleRepository = saleRepository;
            this._unitOfWork = unitOfWork;
        }

        public Sale Add(Sale sale)
        {
            return _saleRepository.Add(sale);
        }

        public Sale Delete(int id)
        {
            return _saleRepository.Delete(id);
        }

        public IEnumerable<Sale> GetAll()
        {
            return _saleRepository.GetAll();
        }

        public IEnumerable<Sale> GetSaleProduct(int id)
        {
            return _saleRepository.GetMulti(x => x.ProductID == id);
        }

        public Sale GetSigleById(int id)
        {
            return _saleRepository.GetSignleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Sale sale)
        {
            _saleRepository.Update(sale);
        }
    }
}
