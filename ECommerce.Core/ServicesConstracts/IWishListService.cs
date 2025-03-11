using ECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts
{
    public interface IWishListService
    {
        public Task<bool> AddToWishList(string userID, string ProductID);
        public Task<bool> RemoveFromWishList(string userID, string ProductID);
        public Task<IEnumerable<ProductDTO>> GetWishListProducts(string userID);
        public Task<bool> IsProductInWishList(string userID ,  string productID);

    }
}
