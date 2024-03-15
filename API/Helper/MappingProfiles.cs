using API.Dtos;
using API.Dtos.identityDto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
                CreateMap<Product, ProductToReturnDto>()
                    .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                    .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                    .ForMember(dest => dest.pictureUrl, opt => opt.MapFrom<ProductUrlResolve>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<BasketItemDto,BasketItem >();
            CreateMap<CustomerBasketDto,CustomerBasket > ();



        }
    }
}
