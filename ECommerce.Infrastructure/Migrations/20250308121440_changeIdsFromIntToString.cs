using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    public partial class changeIdsFromIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotlaPrice",
                table: "Carts",
                newName: "TotalPrice");

            
            // حذف الأعمدة القديمة
            migrationBuilder.DropColumn(name: "Id", table: "WishLists");
            migrationBuilder.DropColumn(name: "Id", table: "ProductsInWishLists");
            migrationBuilder.DropColumn(name: "ProductId", table: "ProductsInWishLists");
            migrationBuilder.DropColumn(name: "WishListId", table: "ProductsInWishLists");
            migrationBuilder.DropColumn(name: "Id", table: "ProductsInCart");
            migrationBuilder.DropColumn(name: "ProductId", table: "ProductsInCart");
            migrationBuilder.DropColumn(name: "CartId", table: "ProductsInCart");
            migrationBuilder.DropColumn(name: "Id", table: "Products");
            migrationBuilder.DropColumn(name: "Id", table: "Images");
            migrationBuilder.DropColumn(name: "ProductId", table: "Images");
            migrationBuilder.DropColumn(name: "Id", table: "Carts");

            // إضافة الأعمدة الجديدة بـ Guid
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "WishLists",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: Guid.NewGuid().ToString());

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ProductsInWishLists",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: Guid.NewGuid().ToString()); 
            
            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "ProductsInWishLists",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false); 
            
            migrationBuilder.AddColumn<string>(
                name: "WishListId",
                table: "ProductsInWishLists",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ProductsInCart",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: Guid.NewGuid().ToString());

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "ProductsInCart",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false);
            
            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "ProductsInCart",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Images",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Products",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: Guid.NewGuid().ToString());

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Images",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: Guid.NewGuid().ToString());

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Carts",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: Guid.NewGuid().ToString());

            // إعادة تعيين المفاتيح الأساسية
            migrationBuilder.AddPrimaryKey(name: "PK_WishLists", table: "WishLists", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_ProductsInWishLists", table: "ProductsInWishLists", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_ProductsInCart", table: "ProductsInCart", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Products", table: "Products", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Images", table: "Images", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Carts", table: "Carts", column: "Id");

            // إعادة تعيين المفاتيح الخارجية
            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInWishLists_WishLists_WishListId",
                table: "ProductsInWishLists",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInCart_Carts_CartId",
                table: "ProductsInCart",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
    name: "FK_ProductsInWishLists_Products",
    table: "ProductsInWishLists",
    column: "ProductId",
    principalTable: "Products",
    principalColumn: "Id",
    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInCart_Products",
                table: "ProductsInCart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Carts",
                newName: "TotlaPrice");

            // إزالة المفاتيح الخارجية
            migrationBuilder.DropForeignKey(name: "FK_ProductsInWishLists_WishLists_WishListId", table: "ProductsInWishLists");
            migrationBuilder.DropForeignKey(name: "FK_ProductsInCart_Carts_CartId", table: "ProductsInCart");

            // إزالة المفاتيح الأساسية
            migrationBuilder.DropPrimaryKey(name: "PK_WishLists", table: "WishLists");
            migrationBuilder.DropPrimaryKey(name: "PK_ProductsInWishLists", table: "ProductsInWishLists");
            migrationBuilder.DropPrimaryKey(name: "PK_ProductsInCart", table: "ProductsInCart");
            migrationBuilder.DropPrimaryKey(name: "PK_Products", table: "Products");
            migrationBuilder.DropPrimaryKey(name: "PK_Images", table: "Images");
            migrationBuilder.DropPrimaryKey(name: "PK_Carts", table: "Carts");

            // حذف الأعمدة الجديدة
            migrationBuilder.DropColumn(name: "Id", table: "WishLists");
            migrationBuilder.DropColumn(name: "Id", table: "ProductsInWishLists");
            migrationBuilder.DropColumn(name: "Id", table: "ProductsInCart");
            migrationBuilder.DropColumn(name: "Id", table: "Products");
            migrationBuilder.DropColumn(name: "Id", table: "Images");
            migrationBuilder.DropColumn(name: "Id", table: "Carts");

            // إعادة الأعمدة القديمة
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WishLists",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductsInWishLists",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductsInCart",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Images",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Carts",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // إعادة تعيين المفاتيح الأساسية والخارجية
            migrationBuilder.AddPrimaryKey(name: "PK_WishLists", table: "WishLists", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_ProductsInWishLists", table: "ProductsInWishLists", column: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInWishLists_WishLists_WishListId",
                table: "ProductsInWishLists",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
