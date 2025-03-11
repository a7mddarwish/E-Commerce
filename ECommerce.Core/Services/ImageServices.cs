using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Http;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.ServicesConstracts.Cloud;
using System.Security.Cryptography.X509Certificates;
using System;

namespace ECommerce.Core.Services
{
    public class ImageServices : IImageServices
    {
        private IImageRepo repo { get; }
        private ICloudService cloud { get; }

        public ImageServices(IImageRepo repo , ICloudService cloud )
        {
            this.repo = repo;
            this.cloud = cloud;
        }
      
        public async Task<string> UploadImageAsync(IFormFile file , string productid ,bool IsPrimary = false)
        {

            // Save to Cloud
            string url = await cloud.UploadImageAsync(file);


            // Save to DB
            repo.Add(new Image {

               ImageUrl = url,
               IsPrimary = IsPrimary,
               ProductId = productid            
           });

            return url;
        }
        



        //Delete form cloud
        public async Task<bool> DeleteFromCloudByIDAsync(string imageId)
        {
            string url = (await repo.FindByIdAsync(imageId)).ImageUrl;

           return await DeleteFromCloudByurlAsync(url);
        }

        public async Task<bool> DeleteFromCloudByurlAsync(string Imageurl)
        {
            return await cloud.DeleteImageAsync(Imageurl);
        }


        // Delete from DB
        public async Task<bool> DeleteFromDBByIDAsync(string imageId)
        {
            repo.DeleteImageByid(imageId);
            return await repo.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFromDBByurlAsync(string Imageurl)
        {
            repo.DeleteImageByurl(Imageurl);

            return await repo.SaveChangesAsync() > 0;
        }

        public async Task<string> GetIDByurl(string Imageurl)
        {
            return (await repo.FindByurlAsync(Imageurl)).Id;
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await repo.SaveChangesAsync() > 0;
        }
    }
}
