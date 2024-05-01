using AutoMapper;
using ir.domain.Entities;
using ir.infrastructure.DTOs.LeadDtos;

namespace ir.infrastructure.MappingProfile
{
    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
           
            CreateMap<LeadCreateDto, Lead>()
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ReverseMap();
            CreateMap<LeadGetDto, Lead>().ReverseMap();
            CreateMap<UpdateLeadStatusDto, Lead>().ReverseMap();
            CreateMap<LeadListDto,Lead>().ReverseMap();
            //CreateMap<AssignSalesRepresentativeDto, Lead>().ReverseMap();
            CreateMap<AssignCustomerToLeadDto, Lead>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));
            CreateMap<Lead, LeadGetDtoWithOpportunities>()
            .ForMember(dest => dest.OpportunityNames, opt => opt.MapFrom(src => src.Opportunities.Select(o => o.OpportunityName)));

        }
    }
}
