using ECommerce.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts
{
    public interface ICategoriesServices
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Category>> GetCatsWithproducts();

        Task<int> Add(Category category);
    }
}
