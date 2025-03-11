using ECommerce.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Domain.Entities;

public partial class WishList
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public Guid UserId { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public virtual AppUser User { get; set; }

    public virtual ICollection<ProductsInWishList> ProductsInWishLists { get; set; } = new List<ProductsInWishList>();
}
