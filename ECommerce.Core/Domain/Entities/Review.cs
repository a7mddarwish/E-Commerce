using ECommerce.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Domain.Entities
{
    public class Review
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Range(0 , 5 , ErrorMessage = "Stars must be between 0 and 5.")]
        public decimal Stars { get; set; } = 0; 

        [MaxLength(1000 , ErrorMessage ="comment must be less than 1000 character")]
        public string ReviewText { get; set; }

        public string ProductID { get; set; }

        public Guid UserID { get; set; }
    }
}
