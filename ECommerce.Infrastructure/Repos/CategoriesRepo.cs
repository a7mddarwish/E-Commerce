using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repos
{
    public class CategoriesRepo : ICategoriesRepo
    {
        private readonly AppDbContext context;
        private readonly DbSet<Category> categories;
        public CategoriesRepo(AppDbContext context)
        {
            this.context = context;
            this.categories = context.Categories;
        }


        public void Add(Category entity)
        {
            categories.Add(entity);
        }

        public void Delete(Category entity)
        {
            categories.Remove(entity);
        }

        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
         return   await categories.Where(predicate).ToListAsync();
        }

        public async Task<Category> FindByIdAsync(int id)
        {
           return await categories.FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCatsWithproducts()
        {
            return await categories.ToListAsync();
         //   return await categories.Include(c => c.Products).ToListAsync();

        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Category entity)
        {
            categories.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

        }
    }
}
