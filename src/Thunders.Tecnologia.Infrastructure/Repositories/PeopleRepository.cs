using Microsoft.EntityFrameworkCore;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Domain.Interfaces;
using Thunders.Tecnologia.Infrastructure.Persistence;

namespace Thunders.Tecnologia.Infrastructure.Repositories;

public class PeopleRepository : IPeopleRepository
{
    private readonly AppDbContext _context;

    public PeopleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<People?> GetByIdAsync(Guid id)
    {
        return await _context.Peoples.FindAsync(id);
    }

    public async Task<IEnumerable<People>> GetAllAsync()
    {
        return await _context.Peoples.ToListAsync();
    }

    public async Task AddAsync(People people)
    {
        await _context.Peoples.AddAsync(people);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(People people)
    {
        _context.Peoples.Update(people);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var people = await GetByIdAsync(id);
        if (people != null)
        {
            _context.Peoples.Remove(people);
            await _context.SaveChangesAsync();
        }
    }
}