using AutoMapper;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesServices categoriesServices;
        private readonly IMapper mapper;

        public CategoriesController(ICategoriesServices categoriesServices, IMapper mapper)
        {
            this.categoriesServices = categoriesServices;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoriesServices.GetAllCategories();

            if (categories == null || categories.Count() == 0)
            {
                return NotFound(new {message = "can not found any data"});
            }


            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCat(CategoryDTO cat)
        {
            Category category = mapper.Map<Category>(cat);

            return ((await categoriesServices.Add(category)) > 0) ? Ok($"{category.Name} added Successfully")
                : StatusCode(500 , (new { message = "Internal server error , cannot save this category" }));

        }

    }
}
