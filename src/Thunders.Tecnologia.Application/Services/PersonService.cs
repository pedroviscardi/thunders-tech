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
    private readonly IPersonRepository _peopleRepository;

    public PersonService(IPersonRepository peopleRepository, IMapper mapper, ILogger<PersonService> logger)
    {
        _peopleRepository = peopleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync()
    {
        _logger.LogInformation("Getting all persons");
        var people = await _peopleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PersonDto>>(people);
    }

    public async Task<PersonDto?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting person with ID: {PersonId}", id);

        var person = await _peopleRepository.GetByIdAsync(id);
        if (person is not null)
        {
            return _mapper.Map<PersonDto>(person);
        }

        _logger.LogWarning("Person with ID: {PersonId} not found", id);

        return null;
    }

    public async Task<Guid> AddAsync(PersonDto personDto)
    {
        _logger.LogInformation("Adding new person with Name: {PersonName} and Email: {PersonEmail}", personDto.Name, personDto.Email);

        var person = _mapper.Map<Person>(personDto);
        await _peopleRepository.AddAsync(person);

        _logger.LogInformation("Person added successfully with ID: {PersonId}", person.Id);

        return person.Id;
    }

    public async Task<bool> UpdateAsync(PersonDto personDto)
    {
        _logger.LogInformation("Updating person with ID: {PersonId}", personDto.Id);

        var person = await _peopleRepository.GetByIdAsync(personDto.Id);
        if (person is null)
        {
            _logger.LogWarning("Person with ID: {PersonId} not found. Update aborted", personDto.Id);
            return false;
        }

        _mapper.Map(personDto, person);
        await _peopleRepository.UpdateAsync(person);

        _logger.LogInformation("Person with ID: {PersonId} updated successfully", personDto.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _logger.LogInformation("Deleting person with ID: {PersonId}", id);

        var person = await _peopleRepository.GetByIdAsync(id);
        if (person is null)
        {
            _logger.LogWarning("Person with ID: {PersonId} not found. Deletion aborted", id);
            return false;
        }

        await _peopleRepository.DeleteAsync(id);

        _logger.LogInformation("Person with ID: {PersonId} deleted successfully", id);

        return true;
    }
}