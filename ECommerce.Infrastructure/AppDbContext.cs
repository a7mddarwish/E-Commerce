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




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ECommerce;User Id=sa;password=sa123456;TrustServerCertificate=True");


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

}
