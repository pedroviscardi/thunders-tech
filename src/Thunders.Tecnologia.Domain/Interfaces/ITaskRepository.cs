using Thunders.Tecnologia.Domain.Entities;

namespace Thunders.Tecnologia.Domain.Interfaces;

public interface ITaskRepository
{
    Task<Tasks?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tasks>> GetAllAsync();
    Task AddAsync(Tasks task);
    Task UpdateAsync(Tasks task);
    Task DeleteAsync(Guid id);
}