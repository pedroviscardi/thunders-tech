using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetAllAsync();
    Task<PersonDto?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(PersonDto data);
    Task<bool> UpdateAsync(PersonDto data);
    Task<bool> DeleteAsync(Guid id);
}