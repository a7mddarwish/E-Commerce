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


        //  public Task<int> NewCart(Cart cart);
        public Task<bool> AddToCart(AddProductCartDTO prodCartDTO , string userid);
        //  public Task<bool> RemoveProductFromCart(int cartId, int productId);
        public Task<short> ProductInuserCart(string userID, int ProductId);
    }
}
