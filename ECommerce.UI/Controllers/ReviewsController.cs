using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace ECommerce.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [HttpPost]
        [Route("AddReview")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> AddReview(AddReviewDTO reviewDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data" });
            string? IDClaim = User.FindFirst(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(IDClaim))
                return Unauthorized(new { message = "Invalid user" });

            Guid.TryParse( IDClaim, out Guid userID);
            if(userID == null)
                return Unauthorized(new { message = "Invalid user" });

            if (await reviewsService.AddReview(reviewDTO, userID))
            {
                return Ok("added successfully");
            }
            return StatusCode(500 , "Internal server error , error occure while save review");
        }

        [HttpGet]
        [Route("UserReview")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserReview(string ProductID)
        {
            string? IDClaim = User.FindFirst(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(IDClaim))
                return Unauthorized(new { message = "Invalid user" });

            Guid.TryParse(IDClaim, out Guid userID);
            if (userID == null)
                return Unauthorized(new { message = "Invalid user" });

            if (userID == null)
                return Unauthorized(new { message = "Invalid user" });
            var review = await reviewsService.UserReview(userID, ProductID);
            if (review == null)
                return NotFound(new { message = "No review found" });
            return Ok(review);
        }


        [HttpPut]
        [Route("UpdateReview")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReview(AddReviewDTO reviewDTO)
        {
           
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data" });
            string? IDClaim = User.FindFirst(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(IDClaim))
                return Unauthorized(new { message = "Invalid user" });

            Guid.TryParse(IDClaim, out Guid userID);
            if (userID == null)
                return Unauthorized(new { message = "Invalid user" });

            if (await reviewsService.UpdateReview(reviewDTO, userID))
            {
                return Ok("updated successfully");
            }
            return StatusCode(500, "Internal server error , error occure while update review");
        }

        [HttpDelete]
        [Route("DeleteReview")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReview(string ProductID)
        {
            string? IDClaim = User.FindFirst(c => c.Type == "UserID")?.Value;


            if (string.IsNullOrEmpty(IDClaim))
                return Unauthorized(new { message = "Invalid user" });

            Guid.TryParse(IDClaim, out Guid userID);
            if (userID == null)
                return Unauthorized(new { message = "Invalid user" });

            if (await reviewsService.DeleteReview(userID, ProductID))
            {
                return Ok("deleted successfully");
            }
            return StatusCode(500, "Internal server error , error occure while delete review");
        }


    }
}
