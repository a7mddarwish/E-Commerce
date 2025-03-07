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

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);
    //    modelBuilder.Entity<Cart>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Carts__3214EC27EFD0DE69");

    //        entity.Property(e => e.Id).HasColumnName("ID");
    //        entity.Property(e => e.Date).HasColumnType("date");
    //        entity.Property(e => e.UserId)
    //            .HasMaxLength(50)
    //            .HasColumnName("UserID");
    //    });

    //    modelBuilder.Entity<Category>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC271B3D8E3A");

    //        entity.Property(e => e.Id).HasColumnName("ID");
    //        entity.Property(e => e.Name).HasMaxLength(70);
    //    });

    //    modelBuilder.Entity<Image>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Images__3214EC2751DF98A8");

    //        entity.Property(e => e.Id).HasColumnName("ID");
    //        entity.Property(e => e.ImageUrl)
    //            .HasMaxLength(255)
    //            .HasColumnName("ImageURL");
    //        entity.Property(e => e.ProductId).HasColumnName("ProductID");

    //        entity.HasOne(d => d.Product).WithMany(p => p.Images)
    //            .HasForeignKey(d => d.ProductId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__Images__ProductI__5CD6CB2B");
    //    });

    //    modelBuilder.Entity<Product>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Products__3214EC27072405A2");

    //        entity.Property(e => e.Id).HasColumnName("ID");
    //        entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
    //        entity.Property(e => e.Name).HasMaxLength(70);
    //        entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

    //        entity.HasOne(d => d.Category).WithMany(p => p.Products)
    //            .HasForeignKey(d => d.CategoryId)
    //            .HasConstraintName("FK__Products__Catego__59FA5E80");
    //    });

    //    modelBuilder.Entity<ProductsInCart>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Products__3214EC27033296E8");

    //        entity.ToTable("ProductsInCart");

    //        entity.Property(e => e.Id).HasColumnName("ID");
    //        entity.Property(e => e.AddedDate).HasColumnType("date");
    //        entity.Property(e => e.CartId).HasColumnName("CartID");
    //        entity.Property(e => e.CurrentPrice).HasColumnType("decimal(10, 2)");
    //        entity.Property(e => e.ProductId).HasColumnName("ProductID");

    //        entity.HasOne(d => d.Cart).WithMany(p => p.ProductsInCart)
    //            .HasForeignKey(d => d.CartId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__ProductsI__CartI__619B8048");

    //        entity.HasOne(d => d.Product).WithMany(p => p.ProductsInCart)
    //            .HasForeignKey(d => d.ProductId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__ProductsI__Produ__628FA481");
    //    });

    //    modelBuilder.Entity<ProductsInWishList>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Products__3214EC27F4C3A2C8");

    //        entity.ToTable("ProductsInWishList");

    //        entity.Property(e => e.Id).HasColumnName("ID");
    //        entity.Property(e => e.AddedDate).HasColumnType("date");
    //        entity.Property(e => e.ProductId).HasColumnName("ProductID");
    //        entity.Property(e => e.WishListId).HasColumnName("WishListID");

    //        entity.HasOne(d => d.Product).WithMany(p => p.ProductsInWishLists)
    //            .HasForeignKey(d => d.ProductId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__ProductsI__Produ__68487DD7");

    //        entity.HasOne(d => d.WishList).WithMany(p => p.ProductsInWishLists)
    //            .HasForeignKey(d => d.WishListId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__ProductsI__WishL__6754599E");
    //    });

    //    modelBuilder.Entity<WishList>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__WishList__3214EC276B5F8548");

    //        entity.Property(e => e.Id).HasColumnName("ID");
    //        entity.Property(e => e.Date).HasColumnType("date");
    //        entity.Property(e => e.UserId).HasColumnName("UserID");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

 //   partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
