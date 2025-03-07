using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddCartProduct(AddProductCartDTO prodCartDTO)
        {
            string userId = User.FindFirst("UserID").Value;
                //chaeck if user input data حلوة
                if (prodCartDTO.quantity < 1)
                return BadRequest(new {message = "Choice valied quantity!"});

                //chaeck if user input data حلوة
                if (string.IsNullOrEmpty(userId) ||!await stock.ProductExist(prodCartDTO.productId))
                return NotFound(new {message ="unable to featch Data , please try later"});

        
                // check if quantity available First
              short productsincart = await cartServ.ProductInuserCart(userId, prodCartDTO.productId);
               if (! await stock.IsAvilableInStock(prodCartDTO.productId , productsincart+prodCartDTO.quantity))
                   return BadRequest(new { message = "this quantity not avliable now !" });



            if(await cartServ.AddToCart(prodCartDTO , userId))
                return Ok(new {message = "Product added sucssefully."});


            return BadRequest(new {message = "somthing went wrong while adding product into cart."});
        }

    }
}
