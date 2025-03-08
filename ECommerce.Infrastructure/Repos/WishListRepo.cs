using AutoMapper;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repos
{
    public class WishListRepo : IWishListRepo
    {
        private readonly AppDbContext context;
        private readonly DbSet<WishList> wishListSet;

        public WishListRepo(AppDbContext context)
        {
            this.context = context;
            wishListSet = context.WishLists;
        }

        public void Add(WishList entity)
        {
            wishListSet.Add(entity);
        }

        public void Delete(WishList entity)
        {
            wishListSet.Remove(entity);

        }

        public async Task<IEnumerable<WishList>> FindAsync(Expression<Func<WishList, bool>> predicate)
        {
            return await wishListSet.Where(predicate).ToListAsync();

        }

        public Task<WishList> GetWishListByUserId(string userID)
        {
            return wishListSet.Include(w => w.ProductsInWishLists).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Update(WishList entity)
        {
            wishListSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
