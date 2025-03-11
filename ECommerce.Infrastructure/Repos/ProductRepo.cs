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
            return await products.Include(p => p.Images).Include(P => P.Reviews).Where(predicate).ToListAsync();
        }

        public async Task<Product> FindByIdAsync(Guid id)
        {
       //     return await products.Include(p => p.Images).FirstOrDefaultAsync(products => products.Id == id.ToString());
           return await products.Include(p => p.Images).Include(P => P.Reviews).FirstOrDefaultAsync(products => products.Id == id.ToString());
        }

        public async Task<int> SaveChangesAsync()
        {
             return await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByCatID(int catID)
        {
            return await products.Include(p => p.Images).Include(P=>P.Reviews).Where(p => p.CategoryId == catID).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCatname(string catname)
        {
            return await products.Include(p => p.Images).Include(p => p.Category).Include(P=>P.Reviews).Where(p => p.Category.Name == catname).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCatName(string catName)
        {
           return await products.Include(p => p.Images).Include(P => P.Reviews).Where(p => p.Category.Name.ToLower() == catName.ToLower()).ToListAsync();
        }

        public async Task<string> AddwithID(Product entity)
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

            // change this to get all products with their images and reviews

            var categories = await context.Categories
                .Include(c => c.Products)
                    .ThenInclude(p => p.Images)
                 .Include(c => c.Products)
                    .ThenInclude(p => p.Reviews)
                .ToListAsync();

            foreach (var Cat in categories)
            {
              
                var Products = Cat.Products.ToList();
                if(Products.Count > 0) 
                RandProducts.Add(Products[random.Next(0, Cat.Products.Count()-1)]);

                
            }

            return RandProducts;

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await products.Include(p => p.Images).Include(P => P.Reviews).ToListAsync();
        }
    }
}
