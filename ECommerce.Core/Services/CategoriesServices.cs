using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.ServicesConstracts;
using System.Runtime.CompilerServices;

namespace ECommerce.Core.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ICategoriesRepo repo;

        public CategoriesServices(ICategoriesRepo repo)
        {
            this.repo = repo;
        }

        public Task<int> Add(Category category)
        {
            repo.Add(category);
            return repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await repo.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetCatsWithproducts()
        {
            return await repo.GetCatsWithproducts();
        }
    }
}
