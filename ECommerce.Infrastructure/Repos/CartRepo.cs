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

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await carts.FindAsync(id);
        }

        public async Task<IReadOnlyList<Cart>> GetAsync(Expression<Func<Cart, bool>> predicate)
        {
            return await carts.Where(predicate).ToListAsync();
        }

        public void Update(Cart entity)
        {
            carts.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            
        }

        public Task<Cart> FindByIdAsync(int id)
        {

            return carts.Include(c => c.ProductsInCarts).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await carts.Include(c => c.ProductsInCarts).ToListAsync();
        }

        public async Task<IEnumerable<Cart>> FindAsync(Expression<Func<Cart, bool>> predicate)
        {
            return await carts.Where(predicate).ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public void AddRange(IEnumerable<Cart> entities)
        {
            carts.AddRange();
        }

        public void DeleteRange(IEnumerable<Cart> entities)
        {
            carts.RemoveRange(entities);

        }

        public async Task<Cart> GetCurrnetCart(string userid)
        {
            return await carts.Include(c => c.ProductsInCarts).FirstOrDefaultAsync(c => c.UserId == userid);
        }

        public void AddProductinCart(ProductsInCart product)
        {
            if (product == null)
                return;

            context.ProductsInCart.Add(product);
        } 
        
        public void RemoveProductFromCart(int productid)
        {
          
            context.ProductsInCart.Remove(context.ProductsInCart.Where(p => p.ProductId == productid).First());
        }

        public async Task<ProductsInCart> GetProductsInCart(int ProductId)
        {
            return await context.ProductsInCart.FirstOrDefaultAsync(p =>  p.ProductId == ProductId);
        }
    }
}
