using System;
using System.Collections.Generic;

namespace ECommerce.Core.Domain.Entities;

public partial class Image
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsPrimary { get; set; }

   // public virtual Product Product { get; set; } = null!;
}
