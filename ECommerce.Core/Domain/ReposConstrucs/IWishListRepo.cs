using ECommerce.Core.Domain.Entities;
using ECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Domain.ReposConstrucs
{
    public interface IWishListRepo : IGenericRepo<WishList>
    {
        public Task<WishList> GetWishListByUserId(string userID);
    }
}
