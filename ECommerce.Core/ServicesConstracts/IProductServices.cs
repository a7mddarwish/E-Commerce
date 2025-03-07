using ECommerce.Core.DTOs;

namespace ECommerce.Core.ServicesConstracts
{
    public interface IProductServices 
    {
        public Task<List<ProductDTO>> GetAll();
        public Task<ProductDTO> AddnewProduct(NewProdutDTO productdto);
        public Task<List<ProductDTO>> GetByCategoryID(int catId);
        public Task<List<ProductDTO>> ExploreProducts();
    }
}
