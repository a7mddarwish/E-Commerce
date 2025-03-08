using AutoMapper;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo cartRepo;
        private readonly IStockService stock;
        private readonly IMapper mapper;

        public CartService(ICartRepo CartRepo , IStockService stock , IMapper mapper)
        {
            cartRepo = CartRepo;
            this.stock = stock;
            this.mapper = mapper;
        }

        public async Task<bool>AddToCart(AddProductCartDTO prodCartDTO , string userId)
        {
            Cart crntcart = await cartRepo.GetCurrnetCart(userId);

            if (crntcart == null)
            {
                crntcart = new Cart();
                crntcart.UserId = userId;
                crntcart.Date = DateTime.Now;
                crntcart.TotalPrice = 0;
                crntcart.IsPaid = false;

                cartRepo.Add(crntcart);
            
               if( await cartRepo.SaveChangesAsync() > 0)
                crntcart = await cartRepo.GetCurrnetCart(userID:crntcart.UserId);

               else
                return false;
            }


            if (crntcart.ProductsInCarts.Any(p => p.Id == prodCartDTO.productId)) 
            {
                ProductsInCart existingprod = await cartRepo.GetProductsInCart(prodCartDTO.productId);

                crntcart.TotalPrice-=existingprod.TotalPrice;
                existingprod.TotalPrice += (prodCartDTO.quantity * existingprod.CurrentPrice);
                existingprod.AddedDate = DateTime.Now;
                existingprod.Quantity += prodCartDTO.quantity;

                crntcart.TotalPrice += existingprod.TotalPrice;

                return (await cartRepo.SaveChangesAsync() > 0);

            }




            Product prod = await stock.GetProduct(prodCartDTO.productId);

            ProductsInCart productCart = new ProductsInCart
            {
                ProductId = prodCartDTO.productId,
                Quantity = prodCartDTO.quantity,
                CurrentPrice = prod.Price,
                TotalPrice = (decimal)((decimal)prodCartDTO.quantity * prod.Price),
                CartId = crntcart.Id,
                AddedDate = DateTime.Now,
            };

            crntcart.TotalPrice += productCart.TotalPrice;

            cartRepo.AddProductinCart(productCart);
            return (await cartRepo.SaveChangesAsync() > 0);
            
            

        }

        public async Task<short> ProductInuserCart(string userID , string ProductId)
        {
            Cart crt = await cartRepo.GetCurrnetCart(userID);
            if (crt == null) return 0;

            if(crt.ProductsInCarts.Any(p => p.ProductId == ProductId))
              return crt.ProductsInCarts.First(x => x.ProductId == ProductId).Quantity;

            return 0;
        }

        public async Task<bool> RemoveProductFromCart(string userID, string ProductId)
        {
            Cart cart = await cartRepo.GetCurrnetCart(userID);
            if (cart == null) return false;

            ProductsInCart prod = cart.ProductsInCarts.FirstOrDefault(p => p.ProductId == ProductId);
            
            if(prod == null) return false;

            cartRepo.RemoveProductFromCart(ProductId);

            return await cartRepo.SaveChangesAsync() > 1;
        }
    }
}
