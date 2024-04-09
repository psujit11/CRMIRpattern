using AutoMapper;
using ir.domain.Entities;
using ir.infrastructure.DTOs.OpportunityDtos;

namespace ir.infrastructure.MappingProfile
{
    public class OpportunityProfile : Profile
    {
        public OpportunityProfile()
        {
            CreateMap<OpportunityCreateDto, Opportunity>().ReverseMap();
            CreateMap<OpportunityGetDto, Opportunity>().ReverseMap();
            CreateMap<OpportunityListDto, Opportunity>().ReverseMap();

        }
    }
}
