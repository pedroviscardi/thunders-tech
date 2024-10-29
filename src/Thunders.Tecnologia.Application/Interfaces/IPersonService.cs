using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetAllAsync();
    Task<PersonDto?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(PersonDto personCreateDto);
    Task<bool> UpdateAsync(PersonDto personUpdateDto);
    Task<bool> DeleteAsync(Guid id);
}