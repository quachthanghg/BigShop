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
    public interface ISlideService
    {
        Slide Add(Slide slide);
        void Update(Slide slide);
        Slide Delete(int id);
        Slide GetSigleById(int id);
        IEnumerable<Slide> GetAll();
        void SaveChanges();
    }

    public class SlideService : ISlideService
    {
        private IErrorService _errorService;
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;
        public SlideService(IErrorService errorService, ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._slideRepository = slideRepository;
            this._unitOfWork = unitOfWork;
        }
        public Slide Add(Slide slide)
        {
            return _slideRepository.Add(slide);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            //return _slideRepository.GetMulti(x => x.Status == true)
            return _slideRepository.GetAll();
        }

        public Slide GetSigleById(int id)
        {
            return _slideRepository.GetSignleById(id);
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}
