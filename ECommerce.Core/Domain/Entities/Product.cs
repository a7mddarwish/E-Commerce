using System;
using System.Collections.Generic;

namespace ECommerce.Core.Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int AvailableInStock { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<ProductsInCart> ProductsInCarts { get; set; } = new List<ProductsInCart>();

    public virtual ICollection<ProductsInWishList> ProductsInWishLists { get; set; } = new List<ProductsInWishList>();
}
