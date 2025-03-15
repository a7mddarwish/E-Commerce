using ECommerce.Core.DTOs;

namespace ECommerce.Core.ServicesConstracts
{
    public interface IProductServices 
    {
        public Task<List<ProductDTO>> GetAll();
        public Task<ProductDTO> AddnewProduct(AddProdutDTO productdto);
        public Task<List<ProductDTO>> GetByCategoryID(int catId);
        public Task<List<ProductDTO>> ExploreProducts();
        public Task<List<ProductDTO>> JustForYouProducts(Guid userid);
        public Task<ProductDTO> Find(Guid ProductID);
        public Task<List<ProductDTO>> GetByCategoryname(string id);
        public Task<bool> IsCategoryExixst(string name);
        public Task<bool> IsCategoryExixst(int id);
    }
}
