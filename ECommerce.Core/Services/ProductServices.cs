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

        public async Task<ProductDTO> AddnewProduct(NewProdutDTO newproductdto)
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

        

            //foreach(var item in images)
            //{
            //    int x = await imageServices.GetIDByurl(item.ImageUrl);
            //   if (! await producyimages.AddNew(new ProductImage { ProductId = product.Id, ImageId = x }))               
            //        return null;               
            //}


            //return new ProductDTO
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    availInStock = product.AvailableInStock,
            //    Description = product.Description,
            //    Price = product.Price,
            //    categoryId = product.CategoryId,
            //    Images = images

            //};



            return mapper.Map<ProductDTO>(product);
        }

        public Task<ProductDTO> AddnewProduct(ProductDTO productdto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDTO>> GetByCategoryID(int catId)
        {
            var products = await repo.GetProductsByCatID(catId);

            // using automapper 
            return mapper.Map<List<ProductDTO>>(products);

        }

        // random product form each category
        public async Task<List<ProductDTO>> ExploreProducts()
        {
            return mapper.Map<List<ProductDTO>>(await repo.ExploreProducts());
        }
    }
}
