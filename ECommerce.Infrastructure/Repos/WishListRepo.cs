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

        public async Task<WishList> GetWishListByUserId(Guid userID)
        {
               return await wishListSet
                .AsSplitQuery()
                .Include(w => w.ProductsInWishLists)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(u => u.UserId == userID);

            

        }

        public WishList GenerateWishlist(Guid userid)
        {
            WishList wsh = new WishList { 
            UserId = userid
            };

            context.Entry(wsh).State = EntityState.Added;

            return wsh;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Update(WishList entity)
        {
            EntityState state = context.Entry(entity).State;

            wishListSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

            EntityState state2 = context.Entry(entity).State;

        }
    }
}
