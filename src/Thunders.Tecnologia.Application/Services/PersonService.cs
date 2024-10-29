using AutoMapper;
using Microsoft.Extensions.Logging;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Domain.Interfaces;

namespace Thunders.Tecnologia.Application.Services;

public class PersonService : IPersonService
{
    private readonly ILogger<PersonService> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository, IMapper mapper, ILogger<PersonService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync()
    {
        _logger.LogInformation("Getting all persons");
        var people = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<PersonDto>>(people);
    }

    public async Task<PersonDto?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting person with ID: {PersonId}", id);

        var person = await _repository.GetByIdAsync(id);
        if (person is not null)
        {
            return _mapper.Map<PersonDto>(person);
        }

        _logger.LogWarning("Person with ID: {PersonId} not found", id);

        return null;
    }

    public async Task<Guid> AddAsync(PersonDto data)
    {
        _logger.LogInformation("Adding new person with Name: {PersonName} and Email: {PersonEmail}", data.Name, data.Email);

        var person = _mapper.Map<Person>(data);
        await _repository.AddAsync(person);

        _logger.LogInformation("Person added successfully with ID: {PersonId}", person.Id);

        return person.Id;
    }

    public async Task<bool> UpdateAsync(PersonDto data)
    {
        _logger.LogInformation("Updating person with ID: {PersonId}", data.Id);

        var person = await _repository.GetByIdAsync(data.Id);
        if (person is null)
        {
            _logger.LogWarning("Person with ID: {PersonId} not found. Update aborted", data.Id);
            return false;
        }

        _mapper.Map(data, person);
        await _repository.UpdateAsync(person);

        _logger.LogInformation("Person with ID: {PersonId} updated successfully", data.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _logger.LogInformation("Deleting person with ID: {PersonId}", id);

        var person = await _repository.GetByIdAsync(id);
        if (person is null)
        {
            _logger.LogWarning("Person with ID: {PersonId} not found. Deletion aborted", id);
            return false;
        }

        await _repository.DeleteAsync(id);

        _logger.LogInformation("Person with ID: {PersonId} deleted successfully", id);

        return true;
    }
}