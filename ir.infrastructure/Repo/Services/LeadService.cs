using AutoMapper;
using FluentValidation;
using ir.domain.Entities;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;
using ir.infrastructure.Repo.Infrastructure;
using ir.infrastructure.Validation;
using Ir.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ir.infrastructure.Repo.Services
{
    public class LeadService : ILeadService
    {
        private readonly AppDbContext _dbContext;
        private readonly Mapper _mapper;
        private readonly IValidator<Lead> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LeadService(AppDbContext dbContext, IMapper mapper,IValidator<Lead> validator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = (Mapper)mapper;
            _validator = validator;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (currentUser == null)
            {
                throw new Exception("No user is currently logged in.");
            }
            lead.ApplicationUserId = currentUser.Id;
            _dbContext.Leads.Add(lead);
            await _dbContext.SaveChangesAsync();
            
            return _mapper.Map<LeadGetDto>(lead);
        }
        public async Task<LeadGetDto> AssignLeadToUserAsync(int leadId, string userId)
        {
            var lead = await _dbContext.Leads.FindAsync(leadId);
            if (lead == null)
            {
                throw new Exception("Lead not found.");
            }

            
            lead.ApplicationUserId = userId;
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<LeadGetDto>(lead);
        }
        public async Task AssignLeadToUserByEmailAsync(int leadId, AssignLeadDto assignLeadDto)
        {
            var user = await _userManager.FindByEmailAsync(assignLeadDto.UserEmail);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Use the user's ID to assign the lead to the user
            await AssignLeadToUserAsync(leadId, user.Id);
        }




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

        public async Task AssignCustomerToLeadAsync(AssignCustomerToLeadDto dto)
        {
            var lead = await _dbContext.Leads.FindAsync(dto.LeadId);
            if (lead == null)
            {
                throw new Exception("Lead not found.");
            }

            _mapper.Map(dto, lead);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<LeadGetDtoWithOpportunities>> GetAllLeadsWithOpportunitiesAsync()
        {
            var leads = await _dbContext.Leads.Include(l => l.Opportunities).ToListAsync();

            // Map each lead to LeadWithOpportunitiesDto
            var leadsWithOpportunitiesDto = leads.Select(lead =>
            {
                var leadDto = _mapper.Map<LeadGetDtoWithOpportunities>(lead);
                leadDto.OpportunityNames = lead.Opportunities.Select(opportunity => opportunity.OpportunityName).ToList();
                return leadDto;
            });

            return leadsWithOpportunitiesDto;
        }
    }
}
