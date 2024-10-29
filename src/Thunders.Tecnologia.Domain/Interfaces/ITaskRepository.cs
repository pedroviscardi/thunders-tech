using Thunders.Tecnologia.Domain.Entities;

namespace Thunders.Tecnologia.Domain.Interfaces;

public interface ITaskRepository
{
    Task<Tasks?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tasks>> GetAllAsync(Guid idPerson);
    Task AddAsync(Tasks task);
    Task UpdateAsync(Tasks task);
    Task DeleteAsync(Guid id);
}