using System;
using System.Collections.Generic;

namespace ECommerce.Core.Domain.Entities;

public partial class ProductsInCart
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.Now;

    public short Quantity { get; set; }

    public decimal CurrentPrice { get; set; }


    public virtual Product Product { get; set; } = null!;

    public decimal TotalPrice { get; set; }

}
