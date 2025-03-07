using AutoMapper;
using ECommerce.Core.Domain.Entities;
using ECommerce.Core.DTOs;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using ECommerce.Core.Domain.IdentityEntities;

namespace ECommerce.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        
         CreateMap<Product, ProductDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Price , opt => opt.MapFrom(src => src.Price))
             .ForMember(dest => dest.availInStock, opt => opt.MapFrom(src => src.AvailableInStock))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.categoryId, opt => opt.MapFrom(src => src.CategoryId))
             .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.ToList()));


            CreateMap<AppUser, userDTO>();

            CreateMap<Category, CategoryDTO>();
        }

    }
}

