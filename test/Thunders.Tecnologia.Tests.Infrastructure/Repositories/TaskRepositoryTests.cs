using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Infrastructure.Persistence;
using Thunders.Tecnologia.Infrastructure.Repositories;

namespace Thunders.Tecnologia.Tests.Infrastructure.Repositories;

public class TaskRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public TaskRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTask_WhenTaskExists()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new TaskRepository(context);

        var task = new Tasks
        {
            IdPerson = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Description",
            CreatedAt = DateTime.UtcNow
        };
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(task.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(task);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTasks()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new TaskRepository(context);

        var idPerson = Guid.NewGuid();
        var tasks = new List<Tasks>
        {
            new()
            {
                IdPerson = idPerson,
                Title = "Test Task",
                Description = "Description 2",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                IdPerson = idPerson,
                Title = "Test Task 2",
                Description = "Description 2",
                CreatedAt = DateTime.UtcNow
            }
        };
        await context.Tasks.AddRangeAsync(tasks);
        await context.SaveChangesAsync();

        var result = await repository.GetAllAsync();

        var enumerable = result.ToList();
        enumerable.Should().NotBeNullOrEmpty().And.HaveCount(tasks.Count);
        enumerable.Should().BeEquivalentTo(tasks);
    }

    [Fact]
    public async Task AddAsync_ShouldAddTask()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new TaskRepository(context);

        var task = new Tasks
        {
            IdPerson = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Description",
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(task);

        var result = await context.Tasks.FindAsync(task.Id);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(task);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingTask()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new TaskRepository(context);

        var task = new Tasks
        {
            IdPerson = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Description",
            CreatedAt = DateTime.UtcNow
        };
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();

        task.Title = "Updated Title";
        await repository.UpdateAsync(task);

        var result = await context.Tasks.FindAsync(task.Id);
        result.Should().NotBeNull();
        result?.Title.Should().Be("Updated Title");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTask_WhenTaskExists()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new TaskRepository(context);

        var task = new Tasks
        {
            IdPerson = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Description",
            CreatedAt = DateTime.UtcNow
        };
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();

        await repository.DeleteAsync(task.Id);

        var result = await context.Tasks.FindAsync(task.Id);
        result.Should().BeNull();
    }
}