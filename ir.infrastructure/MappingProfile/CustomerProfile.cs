using AutoMapper;
using ir.domain.Entities;
using ir.infrastructure.DTOs.CustomerDtos;

namespace ir.infrastructure.MappingProfile
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<CustomerGetDto, Customer>().ReverseMap();
        }
    }
}
