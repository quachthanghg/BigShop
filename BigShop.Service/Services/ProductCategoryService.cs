using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;

namespace BigShop.Service.Services
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);

        void Update(ProductCategory productCategory);

        ProductCategory Delete(int id);

        ProductCategory GetSigleById(int id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAll(string filter);

        IEnumerable<ProductCategory> Search(string filter);

        IEnumerable<ProductCategory> GetAllByParentId(int? parentId);

        void SaveChanges();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            return _productCategoryRepository.Add(productCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return _productCategoryRepository.GetMulti(x => x.Name.Contains(filter));
            }
            else
            {
                return _productCategoryRepository.GetAll();
            }
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int? parentId)
        {
            if (parentId == null)
            {
                return _productCategoryRepository.GetAll();
            }
            else
            {
                return _productCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
            }
        }

        public ProductCategory GetSigleById(int id)
        {
            return _productCategoryRepository.GetSignleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ProductCategory> Search(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return _productCategoryRepository.Search(filter);
            }
            else
            {
                return _productCategoryRepository.GetAll();
            }
        }

        public void Update(ProductCategory productCategory)
        {
            _productCategoryRepository.Update(productCategory);
        }
    }
}