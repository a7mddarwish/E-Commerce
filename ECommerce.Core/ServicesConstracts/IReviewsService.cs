using ECommerce.Core.Domain.Entities;
using ECommerce.Core.DTOs;

namespace ECommerce.Core.ServicesConstracts
{
    public interface IReviewsService
    {
        public Task<bool> AddReview(AddReviewDTO review ,  Guid userId);
        public Task<UserReviewDTO> UserReview(Guid userID, string ProductID);
        /// <summary>
        /// Get the average stars and number of reviews for a product
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public Task<AvgReviewsDTO> Reviewsinfo(string ProductID);
        public Task<bool> UpdateReview(AddReviewDTO reviewDTO, Guid userID);
        public Task<bool> DeleteReview(Guid userID, string productID);
    }
}
