using AutoMapper;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Services
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepo wshRepo;
        private readonly IStockService stock;
        private readonly IMapper mapper;

        public WishListService(IWishListRepo wshRepo , IStockService stock , IMapper mapper)
        {
            this.wshRepo = wshRepo;
            this.stock = stock;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetWishListProducts(string userId)
        {
            Guid.TryParse(userId, out Guid id);

            if (id == Guid.Empty)
                return null;

            WishList wishList = await wshRepo.GetWishListByUserId(id);

           return (wishList != null) ? mapper.Map<IEnumerable<ProductDTO>>(wishList.ProductsInWishLists.Select(p => p.Product)) : null;
        }

        public async Task<bool> IsProductInWishList(string userId, string ProductID)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(ProductID))
                return false;

            Guid.TryParse(userId, out Guid id);

            if (id == Guid.Empty)
                return false;

            WishList userWishList = await wshRepo.GetWishListByUserId(id);

            if (userWishList == null ||! userWishList.ProductsInWishLists.Any(P => P.ProductId == ProductID))
                return false;

            return true;

        }

        public async Task<bool> RemoveFromWishList(string userId, string ProductID)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(ProductID))
                return false;

            Guid.TryParse(userId, out Guid id);

            if (id == Guid.Empty)
                return false;
            WishList userWishList = await wshRepo.GetWishListByUserId(id);

            if (userWishList == null)
                return false;

            if (! userWishList.ProductsInWishLists.Any(p => p.ProductId == ProductID))
                return false;

            ProductsInWishList? ProductToRemove = userWishList.ProductsInWishLists.FirstOrDefault(p => p.ProductId == ProductID);

            if (ProductToRemove == null) return false;


            userWishList.ProductsInWishLists.Remove(ProductToRemove);

            return await wshRepo.SaveChangesAsync() > 0;


        }

        public async Task<bool> AddToWishList(string userId, string productId)
        {
            Guid.TryParse(userId , out Guid id);

            if(id == Guid.Empty)
                return false;

            WishList userWishList = await wshRepo.GetWishListByUserId(id);

           if(userWishList == null) userWishList = wshRepo.GenerateWishlist(id);

           else
           if (userWishList.ProductsInWishLists.Any(p => p.ProductId == productId))
                 return false;
            
             

            userWishList.ProductsInWishLists.Add(new ProductsInWishList
            {
                ProductId = productId,
                WishListId = userWishList.Id
            });

            


            return await wshRepo.SaveChangesAsync() > 0;
        }
    }
}
