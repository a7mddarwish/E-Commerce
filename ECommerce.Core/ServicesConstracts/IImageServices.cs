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
        public Task<string> UploadImageAsync(IFormFile file,string productid, bool IsPrimary = false);

        // Delete from cloud
        public Task<bool> DeleteFromCloudByIDAsync(string imageId);
        public Task<bool> DeleteFromCloudByurlAsync(string Imageurl);

        // Delete form DB
        public Task<bool> DeleteFromDBByIDAsync(string imageId);
        public Task<bool> DeleteFromDBByurlAsync(string Imageurl);



        public Task<string> GetIDByurl(string Imageurl);
    }
}
