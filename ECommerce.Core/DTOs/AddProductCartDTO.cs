using System.Text.Json.Serialization;

namespace ECommerce.Core.DTOs
{
    public class AddProductCartDTO
    {
        

        public string productId { get; set; }
        public short quantity { get; set; }
    }
}
