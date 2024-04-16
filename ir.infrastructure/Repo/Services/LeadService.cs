using AutoMapper;
using FluentValidation;
using ir.domain.Entities;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;
using ir.infrastructure.Repo.Infrastructure;
using ir.infrastructure.Validation;
using Ir.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ir.infrastructure.Repo.Services
{
    public class LeadService : ILeadService
    {
        private readonly AppDbContext _dbContext;
        private readonly Mapper _mapper;
        private readonly IValidator<Lead> _validator;
        public LeadService(AppDbContext dbContext, IMapper mapper,IValidator<Lead> validator)
        {
            _dbContext = dbContext;
            _mapper = (Mapper)mapper;
            _validator = validator;
        }
        public async Task<LeadGetDto> AddAsync(LeadCreateDto createDto)
        {
            var lead = _mapper.Map<Lead>(createDto);
            var validator = new LeadValidator();
            var validationResult = validator.Validate(lead);

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }
            _dbContext.Leads.Add(lead);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<LeadGetDto>(lead);
        }

        /*public async Task AssignToSalesRepresentative(AssignSalesRepresentativeDto dto)
        {
            var lead = await _dbContext.Leads.SingleOrDefaultAsync(l => l.Id == dto.LeadId);
            if (lead != null)
            {
                _mapper.Map(dto, lead);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Lead not found");
            }
        }*/


        public async Task DeleteAsync(int id)
        {
            var lead = await _dbContext.Leads.FindAsync(id);
            if (lead == null)
            {
                throw new Exception("Lead is not Found");
            }
            _dbContext.Leads.Remove(lead);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeadGetDto>> GetAllAsync()
        {
            var leads = await _dbContext.Leads.ToListAsync();
            return _mapper.Map<IEnumerable<LeadGetDto>>(leads);
        }

        public async Task<LeadGetDto> GetByIdAsync(int id)
        {
            var lead = await _dbContext.Leads.FindAsync(id);
            if (lead == null)
            {
                throw new Exception($"No Lead with the  given {id}");
            }
            return _mapper.Map<LeadGetDto>(lead);
        }

        public async Task<IEnumerable<OpportunityListDto>> GetLeadWithOpportunitiesByIdAsync(int id)
        {
            var lead = await _dbContext.Leads
       .Include(l => l.Opportunities)
       .FirstOrDefaultAsync(l => l.Id == id);

            if (lead == null)
            {
                throw new Exception("Lead not found.");
            }

            // Map the opportunities to a list of OpportunityDto
            var opportunitiesDto = _mapper.Map<IEnumerable<OpportunityListDto>>(lead.Opportunities);
            return opportunitiesDto;
        }

        public async Task<LeadGetDto> UpdateAsync(int id, LeadCreateDto updateDto)
        {
            var lead = await _dbContext.Leads.FindAsync(id);
            if (lead == null)
            {
                throw new Exception($"No Lead with the  given {id}");
            }
            _mapper.Map(updateDto, lead);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<LeadGetDto>(lead);
        }

        public async Task UpdateLeadStatus(UpdateLeadStatusDto dto)
        {
            var lead = await _dbContext.Leads.SingleOrDefaultAsync(l => l.Id == dto.LeadId);

            if (lead == null)
            {
                throw new Exception("Lead not found.");
            }

            lead.LeadStatus = dto.NewStatus; 
            await _dbContext.SaveChangesAsync();
        }

    }
}
