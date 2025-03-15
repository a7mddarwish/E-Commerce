using AutoMapper;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;

namespace ECommerce.Core.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepo repo;
        private readonly IImageServices imageServices;
        private readonly IMapper mapper;


        public ProductServices(IProductRepo repo, IImageServices imageServices  , IMapper mapper )
        {
            this.repo = repo;
            this.imageServices = imageServices;
            this.mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            var products = await repo.GetAllAsync();       

            return mapper.Map<List<ProductDTO>>(products);


        }

        public async Task<ProductDTO> AddnewProduct(AddProdutDTO newproductdto)
        {
            // Save Product into Db and get id
            Product product = new Product
            {
                Name = newproductdto.Name,
                Description = newproductdto.Description,
                Price = newproductdto.Price,
                CategoryId = newproductdto.categoryId,
                AvailableInStock = newproductdto.availInStock

            };

            product.Id = await repo.AddwithID(product);

            List<Image> images = new List<Image>();

            //Save First Image as a main image
            string url = await imageServices.UploadImageAsync(newproductdto.formFile[0] ,product.Id ,  true );
            images.Add(new Image { ImageUrl = url, IsPrimary = true , ProductId = product.Id});


            foreach (var form in newproductdto.formFile.Skip(1))
            {
                 url = await imageServices.UploadImageAsync(form ,  product.Id);

                if (string.IsNullOrEmpty(url))
                {
                    Console.WriteLine("somthing went wrong while saving image");
                    return null;
                }
                images.Add(new Image { ImageUrl = url, IsPrimary = false, ProductId = product.Id });
            }

            if (images.Count == 0)
            {
                Console.WriteLine("No images uploaded");
                return null;
            }

 
           if(! await imageServices.SaveChangesAsync())
                repo.Delete(product);

            product.Images = images;

            


        
            return mapper.Map<ProductDTO>(product);
        }
     
        public async Task<List<ProductDTO>> GetByCategoryID(int catId)
        {
            if(catId < 0)
                return null;
            var products = await repo.GetProductsByCatID(catId);

            if (products == null)
                return null;

            // using automapper 
            return mapper.Map<List<ProductDTO>>(products);

        }
        public async Task<List<ProductDTO>> GetByCategoryname(string catName)
        {
            if(string.IsNullOrEmpty(catName))
                return null;

            var products = await repo.GetProductsByCatname(catName);

            if(products == null)
                return null;
            // using automapper 
            return mapper.Map<List<ProductDTO>>(products);

        }

        public async Task<List<ProductDTO>> ExploreProducts()
        {
            return mapper.Map<List<ProductDTO>>(await repo.ExploreProducts());
        }

        public async Task<ProductDTO> Find(Guid ProductID)
        {
            if(ProductID == null)
                return null;

            var product = await repo.FindByIdAsync(ProductID);

            if(product == null)
                return null;

            return mapper.Map<ProductDTO>(product);
        }

        public async Task<List<ProductDTO>> JustForYouProducts(Guid userid)
        {

            return mapper.Map<List<ProductDTO>>(await repo.JustForYou(userid));

        }

        public async Task<bool> IsCategoryExixst(string name)
        {
            return await repo.IsCategoryExist(name);

        }

        public async Task<bool> IsCategoryExixst(int id)
        {
            return await repo.IsCategoryExist(id);
        }
    }
}
