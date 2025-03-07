using ECommerce.Core.Domain.Entities;
using ECommerce.Core.Domain.ReposConstrucs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repos
{
    public class ImageRepo : IImageRepo
    {
        private readonly AppDbContext context;
        private readonly DbSet<Image> images;

        public ImageRepo(AppDbContext context) 
        {
            this.context = context;
            images = context.Images;
        }

        // Add 

        public void Add(Image entity)
        {
            images.Add(entity);
        }

        public void AddRange(IEnumerable<Image> entities)
        {
            images.AddRange(entities);
        }



        // delete

        public void Delete(Image entity)
        {
            images.Remove(entity);
        }

        public void DeleteImageByurl(string Imageurl)
        {
            images.Remove(images.FirstOrDefault(I => I.ImageUrl == Imageurl));
        } 

        public void DeleteImageByid(int id)
        {
            images.Remove(images.FirstOrDefault(I => I.Id == id));
        }

        public void DeleteRange(IEnumerable<Image> entities)
        {
            images.RemoveRange(entities);
        }



        // Find
        public async Task<IEnumerable<Image>> FindAsync(Expression<Func<Image, bool>> predicate)
        {
            return await images.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await images.ToListAsync();
        }

        public async Task<Image> FindByIdAsync(int id)
        {
            return await images.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Image> FindByurlAsync(string url)
        {
            return await images.FirstOrDefaultAsync(i => i.ImageUrl == url);
        }



        //Update
        public void Update(Image entity)
        {
            images.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }



        // Save
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
