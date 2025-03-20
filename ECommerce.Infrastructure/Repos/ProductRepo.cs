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
            return await products.AsSplitQuery()
                .Include(p => p.Images)
                .Include(P => P.Reviews)
                .Where(predicate).ToListAsync();
              
        }

        public async Task<Product> FindByIdAsync(Guid id)
        {
            return await products.AsSplitQuery().
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
            return await products
                .AsSplitQuery()
                .Include(p => p.Images)
                .Include(P => P.Category)
                .Include(P => P.Reviews)
                .Where(p => p.CategoryId == catID).ToListAsync();
        }



        public async Task<IEnumerable<Product>> GetProductsByCatName(string catname)
        {
            return await products.AsSplitQuery().Include(p => p.Images).Include(p => p.Category).Include(P => P.Reviews).Where(p => p.Category.Name == catname).ToListAsync();
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

           
            var categoriesids = await context.Categories.AsNoTracking().Select(c => c.Id).ToListAsync();
            foreach (var Catid in categoriesids)
            {

                var Products = await context.Products.Where(p => p.CategoryId == Catid).ToListAsync();
                
                if (Products.Count > 0)
                    RandProducts.Add(Products[random.Next(0, Products.Count() - 1)]);


            }

            return RandProducts;

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await products
                .AsSplitQuery()
                .Include(p => p.Images)
                .Include(P => P.Category)
                .Include(P => P.Reviews).ToListAsync();
        }


        public async Task<IEnumerable<Product>> JustForYou(Guid userId)
        {
            var categorieIDsInCart = await context.Carts
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.ProductsInCarts.Select(p => p.Product.Category.Id))
                .Distinct()
                .ToListAsync();

            var categorieIDsInWishlist = await context.WishLists
                .Where(w => w.UserId == userId)
                .SelectMany(w => w.ProductsInWishLists.Select(p => p.Product.Category.Id))
                .Distinct()
                .ToListAsync();

            var allCategoriesIDs = categorieIDsInCart.Concat(categorieIDsInWishlist).Distinct().ToList();

            if (!allCategoriesIDs.Any())
                return await ExploreProducts();

            var random = new Random();

            List<Product> recommendedProducts = new List<Product>();
            foreach (int catid in allCategoriesIDs)
            {
                var Prodcutsincat = products.Where(p => p.CategoryId == catid).ToList();
                recommendedProducts.Add( Prodcutsincat[random.Next(0 , Prodcutsincat.Count-1)]);

            }
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
