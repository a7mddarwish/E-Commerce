using System;
using System.Collections.Generic;

namespace ECommerce.Core.Domain.Entities;

public partial class WishList
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<ProductsInWishList> ProductsInWishLists { get; set; } = new List<ProductsInWishList>();
}
