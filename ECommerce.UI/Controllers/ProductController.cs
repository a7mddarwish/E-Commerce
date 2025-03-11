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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromForm] NewProdutDTO product)
        {
         

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newProd = await productsServ.AddnewProduct(product);

                if (newProd == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add product.");
                }

                return Ok(newProd);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [Route("Prod/{productId}")]
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
        [Route("Category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByCategoryid( int id)
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
        
        [HttpGet("Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByCategoryname([FromRoute]string name)
        {
            if(string.IsNullOrEmpty(name))
                return BadRequest(new { message = "Enter valied category name" });

            var products = await productsServ.GetByCategoryname(name);


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
    }
}
