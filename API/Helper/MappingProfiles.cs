using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            {
                CreateMap<Product, ProductToReturnDto>()
                    .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                    .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                    .ForMember(dest => dest.pictureUrl, opt => opt.MapFrom<ProductUrlResolve>());
            }
        }
    }
}
