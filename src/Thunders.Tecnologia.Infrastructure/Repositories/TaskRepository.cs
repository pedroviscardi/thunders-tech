using Microsoft.EntityFrameworkCore;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Domain.Interfaces;
using Thunders.Tecnologia.Infrastructure.Persistence;

namespace Thunders.Tecnologia.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Tasks?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<IEnumerable<Tasks>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task AddAsync(Tasks task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tasks task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}