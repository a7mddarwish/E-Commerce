using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService wishListService;

        public WishListController(IWishListService wishListService)
        {
            this.wishListService = wishListService;
        }

        [HttpPost("Add/{productID}")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToWishList(string productID)
        {
            if (string.IsNullOrWhiteSpace(productID))
                return BadRequest(new { message = "Invalied product ID" });

            string? userID = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userID))
                return Unauthorized(new { message = "Log in First" });

            if (await wishListService.IsProductInWishList(userID, productID))
                return BadRequest(new {message = "this product already in your wishlist"});


            return (await wishListService.AddToWishList(userID, productID)) ?
                Ok("Added successfuly") :
                StatusCode(500, new { message = "internal server error" });

        }

        [HttpDelete("Delete/{productID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFromWishList(string productID)
        {
            if (string.IsNullOrWhiteSpace(productID))
                return BadRequest(new { message = "Invalied product ID" });

            string? userID = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userID))
                return Unauthorized(new { message = "Log in First" });

            if (!await wishListService.IsProductInWishList(userID, productID))
                return BadRequest(new { message = "this product doesnot exist in your wishList"});

            return (await wishListService.RemoveFromWishList(userID, productID)) ?
             Ok("Removed successfuly") :
             StatusCode(500, new { message = "internal server error" });


        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWishListProducts()
        {
            string? userID = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userID))
                return Unauthorized(new { message = "Log in First" });

            var Products = await wishListService.GetWishListProducts(userID);

            return (Products == null) ? NotFound(new {message = "Not found any products"}) : Ok(Products);
                

        }



    }
}
