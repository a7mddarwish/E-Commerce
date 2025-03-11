using ECommerce.Core.DTOs;
using ECommerce.Core.Services;
using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartServ;
        private readonly IStockService stock;

        public CartController(ICartService cartServ , IStockService Stock)
        {
            this.cartServ = cartServ;
            stock = Stock;
        }

        
        [HttpPost("AddToCart")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddCartProduct(AddProductCartDTO prodCartDTO)
        {
            // correct it copilot

            // cast 
             Guid.TryParse((User.FindFirst("UserID").Value) , out Guid userId);
             Guid.TryParse(prodCartDTO.productId, out Guid prodid);
                //chaeck if user input data حلوة
                if (prodCartDTO.quantity < 1)
                return BadRequest(new {message = "Choice valied quantity!"});

                //chaeck if user input data حلوة
                if (userId == null ||!await stock.ProductExist(prodid))
                return NotFound(new {message ="unable to featch Data , please try later"});

        
                // check if quantity available First
              short productsincart = await cartServ.ProductInuserCart(userId.ToString(), prodCartDTO.productId);
               if (! await stock.IsAvilableInStock(prodid, productsincart+prodCartDTO.quantity))
                   return BadRequest(new { message = "this quantity not avliable now !" });



            if(await cartServ.AddToCart(prodCartDTO , userId))
                return Ok(new {message = "Product added sucssefully."});


            return BadRequest(new {message = "somthing went wrong while adding product into cart."});
        }


        [HttpDelete("DeleteFromCart")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveFromCart(string productID)
        {
            if(string.IsNullOrEmpty(productID))
                return BadRequest(new {message = "Enter valied product ID"});

            string? userID = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userID))
                return Unauthorized(new { message = "User Not Found." });

            return (await cartServ.RemoveProductFromCart(userID, productID)) ? Ok("deleted succesfully") : StatusCode(500, new { message = "Internal Server error" });


        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWishListProducts()
        {
            string? userID = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userID))
                return Unauthorized(new { message = "Log in First" });

            var Products = await cartServ.ProductsInCart(userID);

            return (Products == null) ? NotFound(new { message = "Not found any products" }) : Ok(Products);


        }

    }
}
