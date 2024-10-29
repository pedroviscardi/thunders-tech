using AutoMapper;
using FluentAssertions;
using Moq;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Handlers;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Tests.Application.Handlers;

public class PersonCommandQueryHandlersTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPersonService> _personServiceMock;

    public PersonCommandQueryHandlersTests()
    {
        _mapperMock = new Mock<IMapper>();
        _personServiceMock = new Mock<IPersonService>();
    }

    [Fact]
    public async Task CreatePersonCommandHandler_ShouldReturnNewPersonId_WhenPersonIsCreated()
    {
        // Arrange
        var handler = new CreatePersonCommandHandler(_mapperMock.Object, _personServiceMock.Object);
        var command = new CreatePersonCommand("John Doe", "john.doe@example.com", DateTime.Today.AddYears(-30));
        var personDto = new PersonDto {Id = Guid.NewGuid(), Name = command.Name, DateOfBirth = command.DateOfBirth, Email = command.Email};

        _mapperMock.Setup(m => m.Map<PersonDto>(command)).Returns(personDto);
        _personServiceMock.Setup(s => s.AddAsync(personDto)).ReturnsAsync(personDto.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(personDto.Id);
    }

    [Fact]
    public async Task UpdatePersonCommandHandler_ShouldReturnTrue_WhenPersonIsUpdated()
    {
        // Arrange
        var updateHandler = new UpdatePersonCommandHandler(_mapperMock.Object, _personServiceMock.Object);
        var command = new UpdatePersonCommand(Guid.NewGuid(), "John Doe Updated", "john.updated@example.com", DateTime.Today.AddYears(-30));
        var personDto = new PersonDto {Id = command.Id, Name = command.Name, DateOfBirth = command.DateOfBirth, Email = command.Email};

        _mapperMock.Setup(m => m.Map<PersonDto>(command)).Returns(personDto);
        _personServiceMock.Setup(s => s.UpdateAsync(personDto)).ReturnsAsync(true);

        // Act
        var result = await updateHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeletePersonCommandHandler_ShouldReturnTrue_WhenPersonIsDeleted()
    {
        // Arrange
        var deleteHandler = new DeletePersonCommandHandler(_personServiceMock.Object);
        var command = new DeletePersonCommand(Guid.NewGuid());

        _personServiceMock.Setup(s => s.DeleteAsync(command.Id)).ReturnsAsync(true);

        // Act
        var result = await deleteHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdPersonQueryHandler_ShouldReturnPersonDto_WhenPersonExists()
    {
        // Arrange
        var getByIdHandler = new GetByIdPersonQueryHandler(_personServiceMock.Object);
        var query = new GetByIdPersonQuery(Guid.NewGuid());
        var personDto = new PersonDto {Id = query.Id, Name = "Jane Doe", DateOfBirth = DateTime.Today.AddYears(-25), Email = "jane.doe@example.com"};

        _personServiceMock.Setup(s => s.GetByIdAsync(query.Id)).ReturnsAsync(personDto);

        // Act
        var result = await getByIdHandler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(personDto);
    }

    [Fact]
    public async Task GetAllPersonsQueryHandler_ShouldReturnPersonDtoList_WhenPersonsExist()
    {
        // Arrange
        var getAllHandler = new GetAllPersonsQueryHandler(_personServiceMock.Object);
        var query = new GetAllPersonsQuery();
        var personsDto = new List<PersonDto>
        {
            new() {Id = Guid.NewGuid(), Name = "John Doe", DateOfBirth = DateTime.Today.AddYears(-30), Email = "john.doe@example.com"},
            new() {Id = Guid.NewGuid(), Name = "Jane Doe", DateOfBirth = DateTime.Today.AddYears(-25), Email = "jane.doe@example.com"}
        };

        _personServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(personsDto);

        // Act
        var result = await getAllHandler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(personsDto);
    }
}