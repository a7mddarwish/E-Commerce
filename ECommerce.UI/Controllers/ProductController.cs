using ECommerce.Core.Domain.Entities;
using ECommerce.Core.DTOs;
using ECommerce.Core.Services;
using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    [Route("api/Produts")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productsServ;

        public ProductController(IProductServices productsServ)
        {
            this.productsServ = productsServ;
            
        }


        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var products = await productsServ.GetAll();
            if (products == null || products.Count == 0)
            {
                return NotFound(new { message = "cannot featch any data." });
            }

            return Ok(products);
        }



        [HttpPost]
        [Route("Add")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddProduct([FromForm] AddProdutDTO product)
        {

            if (!User.IsInRole("Admin"))           
                return Forbid( "You are not authorized to add a product.");
            

            if (!ModelState.IsValid)            
                return BadRequest(ModelState);


            if (!await productsServ.IsCategoryExixst(product.categoryId))
                return BadRequest(new { message = "Category not found." });



            try
            {
                var newProd = await productsServ.AddnewProduct(product);

                if (newProd == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add product.");
                }
                Guid.TryParse(newProd.Id, out Guid prodId);
                return Created("FindProduct" , new { productId = prodId });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [Route("Prod/{productId}" , Name = "FindProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindProduct(Guid productId)
        {
            if (productId == null)
                return BadRequest(new {message = "Cannnot find this product , try again and inseart valied data"});
            

            ProductDTO prod = await productsServ.Find(productId);
            if(prod == null)
                return NotFound(new { message = "cannot find this product." });

            return Ok(prod);
        }


        [HttpGet]
        [Route("Category/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByCategoryid(int id)
        {
            if(id < 0)
                return BadRequest(new { message = "Enter valied categoryID" });

            var products = await productsServ.GetByCategoryID(id);


            if (products == null || products.Count == 0)
            {
                return NotFound(new {message = "cannot fetch any data."});
            }

            return Ok(products);
        }
        
        [HttpGet("{Categoryname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByCategoryName(string Categoryname)
        {
            if(string.IsNullOrEmpty(Categoryname))
                return BadRequest(new { message = "Enter valied category name" });

            var products = await productsServ.GetByCategoryname(Categoryname);


            if (products == null || products.Count == 0)
            {
                return NotFound(new {message = "cannot fetch any data."});
            }

            return Ok(products);
        }



        [HttpGet]
        [Route("ExploreProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExploreProducts() 
        {
          var products= await productsServ.ExploreProducts();

            if (products == null || products.Count == 0)
            {
                return NotFound(new { message = "cannot catch any data." });
            }

            return Ok(products);
        }
        
        [HttpGet]
        [Route("JustForYou")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> JsutForYou() 
        {

            string? IDClaim = User.FindFirst(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(IDClaim))
                return Unauthorized(new { message = "Register your account First" });

            Guid.TryParse(IDClaim, out Guid userID);
            if (userID == null)
                return Unauthorized(new { message = "Invalid user" });



            var products = await productsServ.JustForYouProducts(userID);

            if (products == null || products.Count == 0)
            {
                return NotFound(new { message = "cannot catch any data." });
            }

            return Ok(products);
        }


    }
}
