using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;

namespace BigShop.Service.Services
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);

        void SaveChanges();
    }

    public class FeedbackService : IFeedbackService
    {
        private IErrorService _errorService;
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IErrorService errorService, IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._errorService = errorService;
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}