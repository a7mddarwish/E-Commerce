using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext context;
        private readonly DbSet<Product> products;

        public ProductRepo(AppDbContext context)
        {
            this.context = context;
            products = context.Set<Product>();
        }



        // دي عمليات بتتم على الذاكرة المؤقته مش لازم تشتغل async
        public void Add(Product entity)
        {
            products.Add(entity);
        }

        public void Update(Product entity)
        {
            products.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

        }

        public void AddRange(IEnumerable<Product> entities)
        {
            products.AddRange(entities);
        }

        public void Delete(Product entity)
        {
            products.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Product> entities)
        {
            products.RemoveRange(entities);
        }



        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
           return await products.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await products.AsNoTracking().Include(p => p.Images).ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await products.FirstOrDefaultAsync(products => products.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
             return await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByCatID(int catID)
        {
            return await products.Include(p => p.Images).Where(p => p.CategoryId == catID).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCatName(string catName)
        {
           return await products.Where(p => p.Category.Name.ToLower() == catName.ToLower()).ToListAsync();
        }

        public async Task<int> AddwithID(Product entity)
        {
            products.Add(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        // Get random product from each category
        public async Task<List<Product>> ExploreProducts()
        {
            List<Product> RandProducts = new List<Product>();
            Random random = new Random();

            var categories = await context.Categories
                .Include(c => c.Products)
                .ThenInclude(p => p.Images)
                .ToListAsync();

            foreach (var Cat in categories)
            {
              
                var Products = Cat.Products.ToList();
                if(Products.Count > 0) 
                RandProducts.Add(Products[random.Next(0, Cat.Products.Count()-1)]);

                
            }

            return RandProducts;

        }
    }
}
