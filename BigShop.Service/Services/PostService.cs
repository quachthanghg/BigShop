using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System.Collections.Generic;

namespace BigShop.Service.Services
{
    public interface IPostService
    {
        Post Add(Post post);

        void Update(Post post);

        Post Delete(int id);

        Post GetSigleById(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByPostCategoryPaging(int postCategory, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    public class PostService : IPostService
    {
        private IErrorService _errorService;
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;

        public PostService(IErrorService errorService, IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }

        public Post Add(Post post)
        {
            return _postRepository.Add(post);
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByPostCategoryPaging(int postCategory, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status == true && x.CategoryID == postCategory, out totalRow, page, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Post GetSigleById(int id)
        {
            return _postRepository.GetSignleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }
    }
}