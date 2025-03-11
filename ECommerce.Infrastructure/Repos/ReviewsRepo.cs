using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repos
{
    public class ReviewsRepo : IReviewsRepo
    {
        private readonly AppDbContext context;
        private readonly DbSet<Review> reviews;

        public ReviewsRepo(AppDbContext context)
        {
            this.context = context;
            reviews = context.Reviews;
        }


        public void AddReview(Review rev)
        {
            reviews.Add(rev);
        }

        public async Task<decimal> AvrgStarsProduct(string productId)
        {
            if(string.IsNullOrEmpty(productId))           
                return 0;            

            var avgStars = await reviews
                .Where(r => r.ProductID == productId)
                .Select(r => (double?)r.Stars)
                .AverageAsync();

            return (decimal)(avgStars ?? 0); 
        }

        public void DeleteReview(Review review)
        {
            reviews.Remove(review);
        }

        public async Task<int> ReviewsNum(string productId)
        {
            return await reviews.Where(r => r.ProductID == productId).CountAsync();
        }


        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<Review?> UserReview(Guid userID, string ProductID)
        {
         return await reviews.Where(R => R.UserID == userID).FirstOrDefaultAsync(R => R.ProductID == ProductID);
        }


    }
}
