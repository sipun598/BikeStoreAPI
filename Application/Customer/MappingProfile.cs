using Application.DTOs;
using Application.DTOs.CustomerDtos;
using AutoMapper;
using Domain.Models;

namespace Application.Customer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customers, CustomersDto>();
            CreateMap(typeof(PaginationViewModel<>), typeof(CustomerPaginationViewModel));

            CreateMap<Products, ProductsDto>()
                .ForMember(pd => pd.BrandName, b => b.MapFrom(s => s.Brand.BrandName))
                .ForMember(pd => pd.CategoryName, c => c.MapFrom(d => d.Category.CategoryName));

            CreateMap(typeof(PaginationViewModel<>), typeof(ProductPaginationViewModel));

            CreateMap<Products, BrandProductsDto>()
              .ForMember(pd => pd.BrandName, b => b.MapFrom(s => s.Brand.BrandName));

            CreateMap<Products, ProductIdNameDto>().ReverseMap();
            CreateMap<Brands, BrandProductsDto>();
        }
    }
}