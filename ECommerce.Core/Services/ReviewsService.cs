using AutoMapper;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;

namespace ECommerce.Core.Services
{

    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepo reviewsRepo;
        private readonly IMapper mapper;

        public ReviewsService(IReviewsRepo reviewsRepo , IMapper mapper)
        {
            this.reviewsRepo = reviewsRepo;
            this.mapper = mapper;
        }

        public async Task<bool> AddReview(AddReviewDTO review , Guid userId)
        {
            reviewsRepo.AddReview(new Review { ProductID = review.ProductID, UserID = userId, Stars = review.Stars, ReviewText = review.ReviewText });
           return (await reviewsRepo.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteReview(Guid userID, string productID)
        {
            // complete this method
            var review = await reviewsRepo.UserReview(userID, productID);
            if (review == null)
                return false;

            reviewsRepo.DeleteReview(review);
            return (await reviewsRepo.SaveChangesAsync()) > 0;
        }

        public async Task<AvgReviewsDTO> Reviewsinfo(string ProductID)
        {
            return new AvgReviewsDTO { avgStars =await reviewsRepo.AvrgStarsProduct(ProductID), ReviewsNum =await reviewsRepo.ReviewsNum(ProductID) };

        }

        public async Task<bool> UpdateReview(AddReviewDTO reviewDTO, Guid userID)
        {
            var Review =  await reviewsRepo.UserReview(userID, reviewDTO.ProductID);
            if (Review == null)
                return false;

            Review.Stars = reviewDTO.Stars;
            Review.ReviewText = reviewDTO.ReviewText;
            return (await reviewsRepo.SaveChangesAsync()) > 0;

        }

        public async Task<UserReviewDTO> UserReview(Guid userID, string ProductID)
        {
            if(userID == null || string.IsNullOrEmpty(ProductID))
                return null;

            Review userReview =await reviewsRepo.UserReview(userID, ProductID);
            if(userReview == null)            
                return null;

            return mapper.Map<UserReviewDTO>(userReview);

        }
    }
}
