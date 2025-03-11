using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECommerce.Core.ServicesConstracts;
using ECommerce.Core.ServicesConstracts.Cloud;
using ECommerce.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Services
{

    public class CloudinaryService : ICloudinaryServices
    {
        private Cloudinary _cloudinary { get; set; }

        public CloudinaryService(IOptions<CloudinarySettings> settings)
        {
            var account = new Account
            {
                Cloud = settings.Value.CloudName,
                ApiKey = settings.Value.ApiKey,
                ApiSecret = settings.Value.ApiSecret,

            };
            var httpclient = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
            _cloudinary = new Cloudinary(account) { Api =  {Client = httpclient } };
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using var stream = file.OpenReadStream();

            string fileExtension = Path.GetExtension(file.FileName);
            string newFileName = $"{Guid.NewGuid()}{fileExtension}";

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(newFileName, stream),
                Transformation = new Transformation().Quality(80).FetchFormat("auto")
            };
            try
            {
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.AbsoluteUri;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading to Cloudinary: {ex.Message}");
                return null;
            }


        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return false;
            try
            {

                var publicId = GetPublicIdFromUrl(imageUrl);
                if (publicId == null) return false;

                var deleteParams = new DeletionParams(publicId);

                DeletionResult result = await _cloudinary.DestroyAsync(deleteParams);

                return result.Result.ToLower() == "ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return false;

            }
        }
        private string GetPublicIdFromUrl(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            return uri.Segments[^1].Split('.')[0]; // استخراج الـ publicId بدون الامتداد
        }
    }
}

