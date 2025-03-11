using ECommerce.Core.Domain.Entities;
using ECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts
{
    public interface ICartService
    {

        public Task<bool> AddToCart(AddProductCartDTO prodCartDTO , Guid userid);
        public Task<bool> RemoveProductFromCart(string userID, string productId);
        public Task<short> ProductInuserCart(string userID,string productId);
        public Task<IEnumerable<ProductDTO>> ProductsInCart(string userID);
    }
}
