using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Services;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Domain.Interfaces;

namespace Thunders.Tecnologia.Tests.Application.Services;

public class TaskServiceTests
{
    private readonly Mock<ILogger<TaskService>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly ITaskService _service;

    public TaskServiceTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<TaskService>>();
        _service = new TaskService(_repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedTasksAndLogMessage_WhenTasksExist()
    {
        // Arrange
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
        var taskDtos = tasks.Select(p => new TaskDto
        {
            Id = p.Id,
            IdPerson = p.IdPerson,
            Title = p.Title,
            Description = p.Description,
            CreatedAt = p.CreatedAt
        }).ToList();
        var logMessage = "Getting all Tasks";

        _repositoryMock.Setup(repo => repo.GetAllAsync(idPerson)).ReturnsAsync(tasks);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<TaskDto>>(tasks)).Returns(taskDtos);

        // Act
        var result = await _service.GetAllAsync(idPerson);

        // Assert
        result.Should().BeEquivalentTo(taskDtos);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessage),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _repositoryMock.Verify(repo => repo.GetAllAsync(idPerson), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnMappedTaskAndLogMessages_WhenTaskExists()
    {
        // Arrange
        var task = new Tasks
        {
            IdPerson = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Description 2",
            CreatedAt = DateTime.UtcNow
        };
        var taskDto = new TaskDto
        {
            Id = task.Id,
            IdPerson = task.IdPerson,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt
        };
        var logMessage = $"Getting Task with ID: {task.Id}";

        _repositoryMock.Setup(repo => repo.GetByIdAsync(task.Id)).ReturnsAsync(task);
        _mapperMock.Setup(mapper => mapper.Map<TaskDto>(task)).Returns(taskDto);

        // Act
        var result = await _service.GetByIdAsync(task.Id);

        // Assert
        result.Should().BeEquivalentTo(taskDto);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessage),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(task.Id), Times.Once);
    }
}