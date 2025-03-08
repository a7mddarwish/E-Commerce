using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Domain.Entities;

public partial class ProductsInCart
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(36)]
    public string CartId { get; set; }

    [MaxLength(36)]
    public string ProductId { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.Now;

    public short Quantity { get; set; }

    public decimal CurrentPrice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public decimal TotalPrice { get; set; }

}
