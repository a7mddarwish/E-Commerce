using ECommerce.Core.Domain.Constractos;
using ECommerce.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ECommerce.Core.Services;
using ECommerce.Core.ServicesConstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ECommerce.Core.ServicesConstracts.Email;

namespace ECommerce.Core
{
    public static class RegCoreServices
    {

        public static IServiceCollection configCoreService(this IServiceCollection service )
        {
            service.RegProductService();
            service.RegImageServices();
            service.RegAutomapperService();
            service.RegCategoriesServices();
            service.RegEmailsender();
            service.RegCartService();
            service.RegStockService();
            service.RegReviewServices();

            return service;
        }

        private static IServiceCollection RegAutomapperService(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(RegCoreServices));
            return service;
        }
        private static IServiceCollection RegProductService(this IServiceCollection service)
        {
            service.AddScoped<IProductServices, ProductServices>();


            return service;
        }
        private static IServiceCollection RegImageServices(this IServiceCollection service)
        {
            return service.AddScoped<IImageServices, ImageServices>();
        }
        private static IServiceCollection RegReviewServices(this IServiceCollection service)
        {
            return service.AddScoped<IReviewsService, ReviewsService>();
        }
        
        private static IServiceCollection RegCategoriesServices(this IServiceCollection service)
        {
            return service.AddScoped<ICategoriesServices, CategoriesServices>();
        }
        private static IServiceCollection RegEmailsender(this IServiceCollection service)
        {

            service.AddTransient<IEmailSender,EmailSender>();
            return service;

        }
       

        private static IServiceCollection RegCartService(this IServiceCollection service)
        {

            service.AddScoped<ICartService , CartService>();
            return service;

        }

        private static IServiceCollection RegStockService(this IServiceCollection service)
        {

            service.AddScoped<IStockService , StockService>();
            return service;

        }


    }
}
