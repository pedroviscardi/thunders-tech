using Microsoft.EntityFrameworkCore;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Domain.Interfaces;
using Thunders.Tecnologia.Infrastructure.Persistence;

namespace Thunders.Tecnologia.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task AddAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var people = await GetByIdAsync(id);
        if (people != null)
        {
            _context.Persons.Remove(people);
            await _context.SaveChangesAsync();
        }
    }
}