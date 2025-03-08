using ECommerce.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts
{
    public interface IStockService
    {
        public Task<bool> IsAvilableInStock(string ProductID, int quantity);
        public Task<bool> ProductExist (string productID);
        public Task<Product> GetProduct(string productID);
    }
}
