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
            service.RegDbcontextService(config);
            service.RegImageRepoService();
            service.RegProductRepoService();
            service.RegCategoriesRepoService();
            service.RegEmailSenderService();
            service.RegCartRepoService();


            return service;

        }

        private static IServiceCollection RegCloudinaryServic(this IServiceCollection service, IConfiguration config)
        {


            service.Configure<CloudinarySettings>(options =>
            config.GetSection("CloudinarySettings").Bind(options));

            service.AddSingleton<ICloudService, CloudinaryService>();

            return service;
        }

        private static IServiceCollection RegDbcontextService(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("connstr"))
            );

            return service;
        }

        private static IServiceCollection RegImageRepoService(this IServiceCollection services)
        {

            services.AddScoped<IImageRepo, ImageRepo>();
            return services;
        }

        private static IServiceCollection RegEmailSenderService(this IServiceCollection services)
        {

            services.AddTransient<IEmailSenderServiceexternalserv,EmailSenderexternalserv>();
            return services;
        }

        private static IServiceCollection RegProductRepoService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepo, ProductRepo>();
            return services;
        }

        private static IServiceCollection RegCategoriesRepoService(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesRepo, CategoriesRepo>();
            return services;
        }

         private static IServiceCollection RegCartRepoService(this IServiceCollection services)
        {
            services.AddScoped<ICartRepo, CartRepo>();
            return services;
        }


    }
}
