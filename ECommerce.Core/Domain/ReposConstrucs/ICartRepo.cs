using ECommerce.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Domain.ReposConstrucs
{
    public interface ICartRepo : IGenericRepo<Cart>
    {
       public Task<Cart>GetCurrnetCart(string userID);
       public void AddProductinCart(ProductsInCart product);
       public void RemoveProductFromCart(int ProductId);
       public Task<ProductsInCart> GetProductsInCart(int ProductId);

    }
}
