using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.DTOs
{
    public class AddReviewDTO
    {
        public string ProductID { get; set; }
        public string? ReviewText { get; set; }
        [Range(0 , 5 , ErrorMessage = "Stars must be between 0 and 5." )]
        public decimal Stars { get; set; }
    }
}
