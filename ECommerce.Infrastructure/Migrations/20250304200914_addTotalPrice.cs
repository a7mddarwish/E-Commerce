using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTotalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {          
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ProductsInCart",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);


            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "TotlaPrice",
                table: "ProductsInCart");

            migrationBuilder.DropColumn(
                name: "TotlaPrice",
                table: "Carts");
   
        }
    }
}
