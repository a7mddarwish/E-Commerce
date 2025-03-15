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

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await products.Include(p => p.Images).Include(P => P.Reviews).Where(predicate).ToListAsync();
        }

        public async Task<Product> FindByIdAsync(Guid id)
        {
            return await products.
                 Include(p => p.Images).
                 Include(P => P.Reviews)
                 .Include(P => P.Category).
                 FirstOrDefaultAsync(products => products.Id == id.ToString());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByCatID(int catID)
        {
            return await products.Include(p => p.Images).Include(P => P.Category).Include(P => P.Reviews).Where(p => p.CategoryId == catID).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCatname(string catname)
        {
            return await products.Include(p => p.Images).Include(p => p.Category).Include(P => P.Reviews).Where(p => p.Category.Name == catname).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCatName(string catName)
        {
            return await products.Include(p => p.Images).Include(P => P.Category).Include(P => P.Reviews).Where(p => p.Category.Name.ToLower() == catName.ToLower()).ToListAsync();
        }

        public async Task<string> AddwithID(Product entity)
        {
            products.Add(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

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
                 .Include(c => c.Products)
                    .ThenInclude(p => p.Category)

                .ToListAsync();

            foreach (var Cat in categories)
            {

                var Products = Cat.Products.ToList();
                if (Products.Count > 0)
                    RandProducts.Add(Products[random.Next(0, Cat.Products.Count() - 1)]);


            }

            return RandProducts;

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await products.Include(p => p.Images).Include(P => P.Category).Include(P => P.Reviews).ToListAsync();
        }


        public async Task<IEnumerable<Product>> JustForYou(Guid userId)
        {
            var categoriesInCart = await context.Carts
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.ProductsInCarts.Select(p => p.Product.Category))
                .Distinct()
                .ToListAsync();

            var categoriesInWishlist = await context.WishLists
                .Where(w => w.UserId == userId)
                .SelectMany(w => w.ProductsInWishLists.Select(p => p.Product.Category))
                .Distinct()
                .ToListAsync();

            var allCategories = categoriesInCart.Concat(categoriesInWishlist).Distinct().ToList();

            if (!allCategories.Any())
                return await ExploreProducts();

            var random = new Random();
            var recommendedProducts = allCategories
                .Select(category => category.Products.OrderBy(p => random.Next()).FirstOrDefault())
                .Where(product => product != null)
                .ToList();

            return recommendedProducts;
        }
        public Task<bool> IsCategoryExist(int id)
        {
            return context.Categories.AnyAsync(c => c.Id == id);
        }

        public Task<bool> IsCategoryExist(string name)
        {
            return context.Categories.AnyAsync(c => c.Name == name);
        }

    }

}
