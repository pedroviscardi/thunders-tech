using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TaskDto?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(TaskDto data);
    Task<bool> UpdateAsync(TaskDto data);
    Task<bool> DeleteAsync(Guid id);
}