using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Domain.Entities;

public partial class ProductsInWishList
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(36)]
    public string WishListId { get; set; }
    
    [MaxLength(36)]
    public string ProductId { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.Now;

    public virtual Product Product { get; set; } = null!;

    public virtual WishList WishList { get; set; } = null!;
}
