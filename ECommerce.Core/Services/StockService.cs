using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.ServicesConstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Services
{

    public class StockService : IStockService
    {
        private readonly IProductRepo productRepo;

        public StockService(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<Product> GetProduct(Guid productID)
        {
           return await productRepo.FindByIdAsync(productID);
        }

        public async Task<bool> IsAvilableInStock(Guid ProductID , int quantity)
        {
            return  (await productRepo.FindByIdAsync(ProductID)).AvailableInStock >= quantity;

        }

        public async Task<bool> ProductExist(Guid productID)
        {
            return (await productRepo.FindByIdAsync(productID)) != null;
        }
    }
}
