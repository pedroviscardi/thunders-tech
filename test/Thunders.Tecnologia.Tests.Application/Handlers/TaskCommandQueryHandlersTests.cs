using AutoMapper;
using FluentAssertions;
using Moq;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Handlers;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Tests.Application.Handlers;

public class TaskCommandQueryHandlersTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ITaskService> _TaskServiceMock;

    public TaskCommandQueryHandlersTests()
    {
        _mapperMock = new Mock<IMapper>();
        _TaskServiceMock = new Mock<ITaskService>();
    }

    [Fact]
    public async Task CreateTaskCommandHandler_ShouldReturnNewTaskId_WhenTaskIsCreated()
    {
        // Arrange
        var handler = new CreateTaskCommandHandler(_mapperMock.Object, _TaskServiceMock.Object);
        var command = new CreateTaskCommand(Guid.NewGuid(), "Test Task", "Test Description");
        var taskDto = new TaskDto
        {
            Id = Guid.NewGuid(),
            IdPerson = command.IdPerson,
            Title = command.Title,
            Description = command.Description,
            CreatedAt = default,
        };

        _mapperMock.Setup(m => m.Map<TaskDto>(command)).Returns(taskDto);
        _TaskServiceMock.Setup(s => s.AddAsync(taskDto)).ReturnsAsync(taskDto.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(taskDto.Id);
    }

    [Fact]
    public async Task UpdateTaskCommandHandler_ShouldReturnTrue_WhenTaskIsUpdated()
    {
        // Arrange
        var updateHandler = new UpdateTaskCommandHandler(_mapperMock.Object, _TaskServiceMock.Object);
        var command = new UpdateTaskCommand(Guid.NewGuid(), "Test Task", "Test Description");
        var taskDto = new TaskDto
        {
            Id = Guid.NewGuid(),
            IdPerson = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            CreatedAt = default,
        };

        _mapperMock.Setup(m => m.Map<TaskDto>(command)).Returns(taskDto);
        _TaskServiceMock.Setup(s => s.UpdateAsync(taskDto)).ReturnsAsync(true);

        // Act
        var result = await updateHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteTaskCommandHandler_ShouldReturnTrue_WhenTaskIsDeleted()
    {
        // Arrange
        var deleteHandler = new DeleteTaskCommandHandler(_TaskServiceMock.Object);
        var command = new DeleteTaskCommand(Guid.NewGuid());

        _TaskServiceMock.Setup(s => s.DeleteAsync(command.Id)).ReturnsAsync(true);

        // Act
        var result = await deleteHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdTaskQueryHandler_ShouldReturnTaskDto_WhenTaskExists()
    {
        // Arrange
        var getByIdHandler = new GetByIdTaskQueryHandler(_TaskServiceMock.Object);
        var query = new GetByIdTaskQuery(Guid.NewGuid());
        var taskDto = new TaskDto
        {
            Id = Guid.NewGuid(),
            IdPerson = Guid.NewGuid(),
            Title = "Test Title",
            Description = "Test Description",
            CreatedAt = DateTime.UtcNow
        };

        _TaskServiceMock.Setup(s => s.GetByIdAsync(query.Id)).ReturnsAsync(taskDto);

        // Act
        var result = await getByIdHandler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(taskDto);
    }

    [Fact]
    public async Task GetAllTasksQueryHandler_ShouldReturnTaskDtoList_WhenTasksExist()
    {
        // Arrange
        var idPerson = Guid.NewGuid();
        var getAllHandler = new GetAllTasksQueryHandler(_TaskServiceMock.Object);
        var query = new GetAllTasksQuery(idPerson);
        var tasksDto = new List<TaskDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                IdPerson = idPerson,
                Title = "Test Title",
                Description = "Test Description",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                IdPerson = idPerson,
                Title = "Test Title",
                Description = "Test Description",
                CreatedAt = DateTime.UtcNow
            }
        };

        _TaskServiceMock.Setup(s => s.GetAllAsync(idPerson)).ReturnsAsync(tasksDto);

        // Act
        var result = await getAllHandler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(tasksDto);
    }
}