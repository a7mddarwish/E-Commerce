using System;
using System.Collections.Generic;
using ECommerce.Core.Domain.IdentityEntities;
using ECommerce.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure;

public partial class AppDbContext : IdentityDbContext<AppUser , AppRoles , Guid>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsInCart> ProductsInCart { get; set; }

    public virtual DbSet<ProductsInWishList> ProductsInWishLists { get; set; }

    public virtual DbSet<WishList> WishLists { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }






    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }


}
