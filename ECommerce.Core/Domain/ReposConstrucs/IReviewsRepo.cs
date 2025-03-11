using ECommerce.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Domain.ReposConstrucs
{
    public interface IReviewsRepo
    {
        public void AddReview(Review rev);
        public Task<Review> UserReview(Guid userID , string ProductID);
        public Task<decimal> AvrgStarsProduct(string ProductID);
        public Task<int> SaveChangesAsync();
        public Task<int> ReviewsNum(string productId);
        public void DeleteReview(Review review);
    }
}
