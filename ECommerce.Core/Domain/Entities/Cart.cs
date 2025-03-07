using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Domain.Entities;

public partial class Cart
{
 
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime Date { get; set; } = DateTime.Now;

    public bool IsPaid { get; set; }

    public decimal TotalPrice { get; set; }

    public ICollection<ProductsInCart> ProductsInCarts { get; set; } = new List<ProductsInCart>();
}
