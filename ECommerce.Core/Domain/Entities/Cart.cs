using ECommerce.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Domain.Entities;

public partial class Cart
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public virtual AppUser User { get; set; }

    public Guid UserId { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public bool IsPaid { get; set; }

    public decimal TotalPrice { get; set; }

    // virtual navigation property to apply lazy loading
    public virtual ICollection<ProductsInCart> ProductsInCarts { get; set; } = new List<ProductsInCart>();
}
