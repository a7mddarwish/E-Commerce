using System;
using System.Collections.Generic;

namespace ECommerce.Core.Domain.Entities;

public partial class ProductsInWishList
{
    public int Id { get; set; }

    public int WishListId { get; set; }

    public int ProductId { get; set; }

    public DateTime AddedDate { get; set; }

    public short Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual WishList WishList { get; set; } = null!;
}
