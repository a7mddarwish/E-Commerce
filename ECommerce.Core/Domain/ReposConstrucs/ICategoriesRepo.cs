using ECommerce.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Domain.ReposConstrucs
{
    public interface ICategoriesRepo : IGenericRepo<Category>
    {
        public Task<IEnumerable<Category>> GetCatsWithproducts();
        public Task<IEnumerable<Category>> GetAllAsync();
       

    }
}
