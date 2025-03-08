using ECommerce.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ECommerce.Core.Domain.ReposConstrucs;
using ECommerce.Infrastructure.Services;
using ECommerce.Core.ServicesConstracts.Cloud;
using ECommerce.Infrastructure.Repos;
using ASPNETCoreIdentityDemo.Services;
using ECommerce.Core.ServicesConstracts.Email;

namespace ECommerce.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfrastructureService(this IServiceCollection service, IConfiguration config)
        {
            service.RegCloudinaryServic(config);
            service.RegDbcontextRepo(config);
            service.RegImageRepo();
            service.RegProductRepo();
            service.RegCategoriesRepo();
            service.RegEmailSender();
            service.RegCartRepo();
            service.RegWishListRepo();


            return service;

        }

        private static IServiceCollection RegCloudinaryServic(this IServiceCollection service, IConfiguration config)
        {


            service.Configure<CloudinarySettings>(options =>
            config.GetSection("CloudinarySettings").Bind(options));

            service.AddSingleton<ICloudService, CloudinaryService>();

            return service;
        }

        private static IServiceCollection RegDbcontextRepo(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("connstr"))
            );

            return service;
        }

        private static IServiceCollection RegImageRepo(this IServiceCollection services)
        {

            services.AddScoped<IImageRepo, ImageRepo>();
            return services;
        }

        private static IServiceCollection RegEmailSender(this IServiceCollection services)
        {

            services.AddTransient<IEmailSenderServiceexternalserv,EmailSenderexternalserv>();
            return services;
        }

        private static IServiceCollection RegProductRepo(this IServiceCollection services)
        {
            services.AddScoped<IProductRepo, ProductRepo>();
            return services;
        }

        private static IServiceCollection RegCategoriesRepo(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesRepo, CategoriesRepo>();
            return services;
        }

        private static IServiceCollection RegCartRepo(this IServiceCollection services)
        {
            services.AddScoped<ICartRepo, CartRepo>();
            return services;
        }

        private static IServiceCollection RegWishListRepo(this IServiceCollection services)
        {
            services.AddScoped<IWishListRepo, WishListRepo>();
            return services;
        }



    }
}
