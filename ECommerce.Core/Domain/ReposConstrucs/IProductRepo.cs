using ECommerce.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Domain.ReposConstrucs
{
    public interface IProductRepo :IGenericRepo<Product>
    {
        public Task<IEnumerable<Product>> GetProductsByCatID(int catID);
        public Task<List<Product>> ExploreProducts();
        public Task<IEnumerable<Product>> GetProductsByCatName(string catName);
        public Task<int> AddwithID(Product entity);

    }
}
