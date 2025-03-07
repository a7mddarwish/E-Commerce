using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts.Cloud
{
    public interface ICloudService
    {
        // will return the new image url
        public Task<string> UploadImageAsync(IFormFile file);
        public Task<bool> DeleteImageAsync(string imageUrl);

    }
}
