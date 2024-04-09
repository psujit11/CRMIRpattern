using AutoMapper;
using ir.domain.Entities;
using ir.infrastructure.DTOs.LeadDtos;

namespace ir.infrastructure.MappingProfile
{
    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            CreateMap<LeadCreateDto, Lead>().ReverseMap();
            CreateMap<LeadGetDto, Lead>().ReverseMap();
            CreateMap<UpdateLeadStatusDto, Lead>().ReverseMap();
            CreateMap<LeadListDto,Lead>().ReverseMap();
            //CreateMap<AssignSalesRepresentativeDto, Lead>().ReverseMap();

        }
    }
}
