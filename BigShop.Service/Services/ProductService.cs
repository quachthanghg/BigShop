﻿using BigShop.Common;
using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace BigShop.Service.Services
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        Product GetSigleById(int id);

        IEnumerable<Product> Search(string filter);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string filter);

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetHotProduct(int top);
        IEnumerable<Product> Sort(string price, string alias);

        IEnumerable<Product> GetRelatedProducts(int id, int top);

        ProductCategory GetParentID(string alias);

        IEnumerable<Product> GetListProductByCategoryPaging(string sort, string alias, int page, int pageSize, out int totalRow);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<string> GetListProductByName(string name);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IProductTagRepository productTagRepository, ITagRepository tagRepository, IProductCategoryRepository productCategoryRepository)
        {
            this._productRepository = productRepository;
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
        }

        public Product Add(Product product)
        {
            var products = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.productTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return products;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return _productRepository.GetMulti(x => x.Name.Contains(filter), new string[] { "PostCategory" });
            }
            else
            {
                return _productRepository.GetAll(new string[] { "PostCategory" });
            }
        }

        public ProductCategory GetParentID(string alias)
        {
            if (string.IsNullOrEmpty(alias)) return null;
            var category = _productCategoryRepository.GetSignleByCondition(x=>x.Alias == alias);
            var lstCategory = _productCategoryRepository.GetSignleByCondition(p => p.ParentID == category.ID);
            
            return lstCategory;
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.HotFlag == true, new string[] { "ProductCategory" }).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true, new string[] { "ProductCategory" }).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryPaging(string sort, string alias, int page, int pageSize, out int totalRow)
        {
            var category = _productCategoryRepository.GetSignleByCondition(x => x.Alias == alias);
            if (string.IsNullOrEmpty(sort))
            {
                var query = _productRepository.GetMulti(x => x.ProductCategory != null && (x.ProductCategory.Alias == alias || x.ProductCategory.ParentID == category.ID));
                totalRow = query.Count();
                return query.Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                List<Product> result = new List<Product>();
                List<Product> resultCategory = new List<Product>();

                string[] strs = sort.Split(new char[] { '|' });

                string[] sortPrice= { };
                string[] sortCategory= { };

                if (strs.Length == 1)
                {
                    sortPrice = strs[0].Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    sortPrice = strs[0].Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                    sortCategory = strs[1].Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                }

                if (sortCategory.Length > 0)
                {
                    foreach (var cat in sortCategory)
                    {
                        var query = _productRepository.GetMulti(x => x.ProductCategory != null && (x.ProductCategory.Alias == alias || x.ProductCategory.ParentID == category.ID) &&  x.ProductCategory.Name == cat);
                        resultCategory.AddRange(query);
                    }
                }
                else
                {
                    resultCategory = _productRepository.GetMulti(x => x.ProductCategory != null && (x.ProductCategory.Alias == alias || x.ProductCategory.ParentID == category.ID)).ToList();
                }

                if (sortPrice.Length > 0)
                {
                    foreach (var item in sortPrice)
                    {
                        var maxPrice = decimal.Parse(item);
                        var minPrice = maxPrice - 5000000;
                        var query = resultCategory.Where(x => x.ProductCategory != null && (x.ProductCategory.Alias == alias || x.ProductCategory.ParentID == category.ID) && x.Price <= maxPrice && x.Price >= minPrice);
                        result.AddRange(query);
                    }
                }
                else result = resultCategory;
                
                totalRow = result.Count();
                return result.Skip((page - 1) * pageSize).Take(pageSize);
            }
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _productRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int top)
        {
            var product = _productRepository.GetSignleById(id);

            return _productRepository.GetMulti(x => x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public Product GetSigleById(int id)
        {
            return _productRepository.GetSignleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Name.Contains(keyword));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> Search(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return _productRepository.Search(filter);
            }
            else
            {
                return _productRepository.GetAll();
            }
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.productTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> Sort(string price, string alias)
        {
            var model = _productRepository.GetMulti(x => x.Price == decimal.Parse(price) && x.ProductCategory.Alias == alias && x.Status == true);
            throw new System.NotImplementedException();
        }
    }
}