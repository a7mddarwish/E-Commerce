using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts
{
    public interface IImageServices
    {
        public Task<string> UploadImageAsync(IFormFile file,int productid, bool IsPrimary = false);

        // Delete from cloud
        public Task<bool> DeleteFromCloudByIDAsync(int imageId);
        public Task<bool> DeleteFromCloudByurlAsync(string Imageurl);

        // Delete form DB
        public Task<bool> DeleteFromDBByIDAsync(int imageId);
        public Task<bool> DeleteFromDBByurlAsync(string Imageurl);



        public Task<int> GetIDByurl(string Imageurl);
    }
}
