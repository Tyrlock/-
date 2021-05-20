using CoreLibarary.Interfaces;
using CoreLibarary.Models;
using System.Collections.ObjectModel;

namespace CoreLibarary.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _ReviewsRepository;

        private ObservableCollection<Reviews> _Reviews;

        public ObservableCollection<Reviews> Reviews
        {
            get
            {
                return _Reviews;
            }
        }

        public ReviewsService(IReviewsRepository reviewsRepository)
        {
            _ReviewsRepository = reviewsRepository;
            _Reviews = Get();
        }



        public int? Create(int creatidBy, string description, int RecepientId)
        {
            var reviewid = _ReviewsRepository.Create(creatidBy,description, RecepientId);
            return reviewid;
        }

        public ObservableCollection<Reviews> Get()
        {
            var review = new ObservableCollection<Reviews>(_ReviewsRepository.Get());
            return review;
        }
    }
}
