using Azure.Core;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repos
{
    public class CartRepo : ICartRepo
    {
        private readonly AppDbContext context;
        private readonly DbSet<Cart> carts;

        public CartRepo(AppDbContext context)
        {
            this.context = context;
            carts = context.Carts;

        }

        public void Add(Cart entity)
        {
            carts.Add(entity);
        }

        public void Delete(Cart entity)
        {
            carts.Remove(entity);
        }       

        public async Task<Cart> GetByIdAsync(string id)
        {
            return await carts.FindAsync(id);
        }

        public void Update(Cart entity)
        {
            carts.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            
        }

        public Task<Cart> FindByIdAsync(string id)
        {

            return carts.Include(c => c.ProductsInCarts).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cart>> FindAsync(Expression<Func<Cart, bool>> predicate)
        {
            return await carts.Where(predicate).ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public async Task<Cart> GetCurrnetCart(string userid)
        {
            return await carts.Include(c => c.ProductsInCarts).ThenInclude(p => p.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync(c => c.UserId.ToString() == userid);
        }

        public void AddProductinCart(ProductsInCart product)
        {
            if (product == null)
                return;

            context.ProductsInCart.Add(product);
        } 
        
        public void RemoveProductFromCart(string productid)
        {
          
            context.ProductsInCart.Remove(context.ProductsInCart.Where(p => p.ProductId == productid).First());
        }

        public async Task<ProductsInCart> GetProductsInCart(string ProductId)
        {
            return await context.ProductsInCart.Include(p => p.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync(p =>  p.ProductId == ProductId);
        }
    }
}
