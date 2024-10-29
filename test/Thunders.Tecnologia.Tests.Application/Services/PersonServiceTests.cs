using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Services;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Domain.Interfaces;

namespace Thunders.Tecnologia.Tests.Application.Services;

public class PersonServiceTests
{
    private readonly Mock<ILogger<PersonService>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly PersonService _personService;

    public PersonServiceTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<PersonService>>();
        _personService = new PersonService(_personRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedPersonsAndLogMessage_WhenPersonsExist()
    {
        // Arrange
        var persons = new List<Person> {new() {Id = Guid.NewGuid(), Name = "John Doe", DateOfBirth = DateTime.Today.AddYears(-30), Email = "john.doe@example.com"}};
        var personDtos = persons.Select(p => new PersonDto {Id = p.Id, Name = p.Name, DateOfBirth = p.DateOfBirth, Email = p.Email}).ToList();
        var logMessage = "Getting all persons";

        _personRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(persons);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<PersonDto>>(persons)).Returns(personDtos);

        // Act
        var result = await _personService.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(personDtos);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessage),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _personRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnMappedPersonAndLogMessages_WhenPersonExists()
    {
        // Arrange
        var person = new Person {Id = Guid.NewGuid(), Name = "Jane Doe", DateOfBirth = DateTime.Today.AddYears(-25), Email = "jane.doe@example.com"};
        var personDto = new PersonDto {Id = person.Id, Name = person.Name, DateOfBirth = person.DateOfBirth, Email = person.Email};
        var logMessage = $"Getting person with ID: {person.Id}";

        _personRepositoryMock.Setup(repo => repo.GetByIdAsync(person.Id)).ReturnsAsync(person);
        _mapperMock.Setup(mapper => mapper.Map<PersonDto>(person)).Returns(personDto);

        // Act
        var result = await _personService.GetByIdAsync(person.Id);

        // Assert
        result.Should().BeEquivalentTo(personDto);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessage),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _personRepositoryMock.Verify(repo => repo.GetByIdAsync(person.Id), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnPersonIdAndLogMessages_WhenPersonIsAdded()
    {
        // Arrange
        var personDto = new PersonDto {Name = "John Doe", DateOfBirth = DateTime.Today.AddYears(-30), Email = "john.doe@example.com"};
        var person = new Person {Id = Guid.NewGuid(), Name = personDto.Name, DateOfBirth = personDto.DateOfBirth, Email = personDto.Email};
        var logMessageAdd = $"Adding new person with Name: {personDto.Name} and Email: {personDto.Email}";
        var logMessageSuccess = $"Person added successfully with ID: {person.Id}";

        _mapperMock.Setup(mapper => mapper.Map<Person>(personDto)).Returns(person);
        _personRepositoryMock.Setup(repo => repo.AddAsync(person)).Returns(Task.CompletedTask);

        // Act
        var result = await _personService.AddAsync(personDto);

        // Assert
        result.Should().Be(person.Id);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessageAdd),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessageSuccess),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _personRepositoryMock.Verify(repo => repo.AddAsync(person), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrueAndLogMessages_WhenPersonIsUpdated()
    {
        // Arrange
        var personDto = new PersonDto {Id = Guid.NewGuid(), Name = "John Updated", DateOfBirth = DateTime.Today.AddYears(-28), Email = "john.updated@example.com"};
        var person = new Person {Id = personDto.Id, Name = "John Doe", DateOfBirth = DateTime.Today.AddYears(-30), Email = "john.doe@example.com"};
        var logMessageUpdate = $"Updating person with ID: {personDto.Id}";
        var logMessageSuccess = $"Person with ID: {personDto.Id} updated successfully";

        _personRepositoryMock.Setup(repo => repo.GetByIdAsync(personDto.Id)).ReturnsAsync(person);
        _mapperMock.Setup(mapper => mapper.Map(personDto, person));
        _personRepositoryMock.Setup(repo => repo.UpdateAsync(person)).Returns(Task.CompletedTask);

        // Act
        var result = await _personService.UpdateAsync(personDto);

        // Assert
        result.Should().BeTrue();

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessageUpdate),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessageSuccess),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _personRepositoryMock.Verify(repo => repo.UpdateAsync(person), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrueAndLogMessages_WhenPersonIsDeleted()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var person = new Person {Id = personId, Name = "Jane Doe", DateOfBirth = DateTime.Today.AddYears(-25), Email = "jane.doe@example.com"};
        var logMessageDelete = $"Deleting person with ID: {personId}";
        var logMessageSuccess = $"Person with ID: {personId} deleted successfully";

        _personRepositoryMock.Setup(repo => repo.GetByIdAsync(personId)).ReturnsAsync(person);
        _personRepositoryMock.Setup(repo => repo.DeleteAsync(personId)).Returns(Task.CompletedTask);

        // Act
        var result = await _personService.DeleteAsync(personId);

        // Assert
        result.Should().BeTrue();

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessageDelete),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString() == logMessageSuccess),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), Times.Once);

        _personRepositoryMock.Verify(repo => repo.DeleteAsync(personId), Times.Once);
    }
}