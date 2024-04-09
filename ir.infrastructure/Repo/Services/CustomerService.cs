using AutoMapper;
using ir.domain.Entities;
using ir.infrastructure.DTOs.CustomerDtos;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;
using ir.infrastructure.Repo.Infrastructure;
using Ir.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ir.infrastructure.Repo.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _dbContext;
        private readonly Mapper _mapper;

        public CustomerService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = (Mapper)mapper;
        }
        public async Task<CustomerGetDto> AddAsync(CustomerCreateDto createDto)
        {
            var customer = _mapper.Map<Customer>(createDto);
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CustomerGetDto>(customer);

        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer is not found");
            }
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerGetDto>> GetAllAsync()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerGetDto>>(customers);
        }

        public async Task<CustomerGetDto> GetByIdAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception($"No Customer with given {id}");
            }
            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task<IEnumerable<LeadListDto>> GetLeadsofCustomersByIdAsync(int id)
        {
            var customerLeadList = await _dbContext.Customers.Include(l => l.Leads).FirstOrDefaultAsync(l => l.Id == id);

            if (customerLeadList == null)
            {
                throw new Exception("Lead not found.");
            }

            // Map the opportunities to a list of OpportunityDto
            var leadsDto = _mapper.Map<IEnumerable<LeadListDto>>(customerLeadList.Leads);
            return leadsDto;
        }

        public async Task<CustomerGetDto> UpdateAsync(int id, CustomerCreateDto updateDto)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception($"No Customer with given {id}");
            }
            _mapper.Map(updateDto, customer);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CustomerGetDto>(customer);
        }
    }
}
