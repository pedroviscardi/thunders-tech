using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Infrastructure.Persistence;
using Thunders.Tecnologia.Infrastructure.Repositories;

namespace Thunders.Tecnologia.Tests.Infrastructure.Repositories;

public class PersonRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public PersonRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPerson_WhenPersonExists()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new PersonRepository(context);

        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = DateTime.Today.AddYears(-30),
            Email = "john.doe@example.com"
        };
        await context.Peoples.AddAsync(person);
        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(person.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(person);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPersons()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new PersonRepository(context);

        var persons = new List<Person>
        {
            new() {Id = Guid.NewGuid(), Name = "Jane Doe", DateOfBirth = DateTime.Today.AddYears(-25), Email = "jane.doe@example.com"},
            new() {Id = Guid.NewGuid(), Name = "John Smith", DateOfBirth = DateTime.Today.AddYears(-28), Email = "john.smith@example.com"}
        };
        await context.Peoples.AddRangeAsync(persons);
        await context.SaveChangesAsync();

        var result = await repository.GetAllAsync();

        var enumerable = result.ToList();
        enumerable.Should().NotBeNullOrEmpty().And.HaveCount(persons.Count);
        enumerable.Should().BeEquivalentTo(persons);
    }

    [Fact]
    public async Task AddAsync_ShouldAddPerson()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new PersonRepository(context);

        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Alice Doe",
            DateOfBirth = DateTime.Today.AddYears(-22),
            Email = "alice.doe@example.com"
        };

        await repository.AddAsync(person);

        var result = await context.Peoples.FindAsync(person.Id);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(person);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingPerson()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new PersonRepository(context);

        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Bob Doe",
            DateOfBirth = DateTime.Today.AddYears(-35),
            Email = "bob.doe@example.com"
        };
        await context.Peoples.AddAsync(person);
        await context.SaveChangesAsync();

        person.Name = "Updated Bob Doe";
        await repository.UpdateAsync(person);

        var result = await context.Peoples.FindAsync(person.Id);
        result.Should().NotBeNull();
        result?.Name.Should().Be("Updated Bob Doe");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemovePerson_WhenPersonExists()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new PersonRepository(context);

        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Charlie Doe",
            DateOfBirth = DateTime.Today.AddYears(-40),
            Email = "charlie.doe@example.com"
        };
        await context.Peoples.AddAsync(person);
        await context.SaveChangesAsync();

        await repository.DeleteAsync(person.Id);

        var result = await context.Peoples.FindAsync(person.Id);
        result.Should().BeNull();
    }
}