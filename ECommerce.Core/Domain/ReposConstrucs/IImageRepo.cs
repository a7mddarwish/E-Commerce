using ECommerce.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Core.Domain.ReposConstrucs
{
    public interface IImageRepo : IGenericRepo<Image>
    {
        public void DeleteImageByurl(string Imageurl);
        public void DeleteImageByid(int id);
        public Task<Image> FindByurlAsync(string url);
    }
}
