using Thunders.Tecnologia.Domain.Entities;

namespace Thunders.Tecnologia.Domain.Interfaces;

public interface IPeopleRepository
{
    Task<People?> GetByIdAsync(Guid id);
    Task<IEnumerable<People>> GetAllAsync();
    Task AddAsync(People people);
    Task UpdateAsync(People people);
    Task DeleteAsync(Guid id);
}