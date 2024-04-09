using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.infrastructure.Repo.Infrastructure
{
    public interface IGeneralCrudService<T, TCreateDto, TGetDto> where T : class
    {
        Task<TGetDto> GetByIdAsync(int id);
        Task<IEnumerable<TGetDto>> GetAllAsync();
        Task<TGetDto> AddAsync(TCreateDto createDto);
        Task<TGetDto> UpdateAsync(int id, TCreateDto updateDto);
        Task DeleteAsync(int id);
    }
}
