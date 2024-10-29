using AutoMapper;
using Microsoft.Extensions.Logging;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Domain.Entities;
using Thunders.Tecnologia.Domain.Interfaces;

namespace Thunders.Tecnologia.Application.Services;

public class TaskService : ITaskService
{
    private readonly ILogger<TaskService> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository, IMapper mapper, ILogger<TaskService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        _logger.LogInformation("Getting all Tasks");
        var people = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TaskDto>>(people);
    }

    public async Task<TaskDto?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting Task with ID: {TaskId}", id);

        var task = await _repository.GetByIdAsync(id);
        if (task is not null)
        {
            return _mapper.Map<TaskDto>(task);
        }

        _logger.LogWarning("Task with ID: {TaskId} not found", id);

        return null;
    }

    public async Task<Guid> AddAsync(TaskDto data)
    {
        _logger.LogInformation("Adding new Task with Title: {TaskTitle} and IdPerson: {TaskIdPerson}", data.Title, data.IdPerson);

        var task = _mapper.Map<Tasks>(data);
        task.CreatedAt = DateTime.UtcNow;
        task.IsCompleted = false;
        await _repository.AddAsync(task);

        _logger.LogInformation("Task added successfully with ID: {TaskId}", task.Id);

        return task.Id;
    }

    public async Task<bool> UpdateAsync(TaskDto data)
    {
        _logger.LogInformation("Updating Task with ID: {TaskId}", data.Id);

        var task = await _repository.GetByIdAsync(data.Id);
        if (task is null)
        {
            _logger.LogWarning("Task with ID: {TaskId} not found. Update aborted", data.Id);
            return false;
        }

        _mapper.Map(data, task);
        task.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(task);

        _logger.LogInformation("Task with ID: {TaskId} updated successfully", data.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _logger.LogInformation("Deleting Task with ID: {TaskId}", id);

        var task = await _repository.GetByIdAsync(id);
        if (task is null)
        {
            _logger.LogWarning("Task with ID: {TaskId} not found. Deletion aborted", id);
            return false;
        }

        await _repository.DeleteAsync(id);

        _logger.LogInformation("Task with ID: {TaskId} deleted successfully", id);

        return true;
    }
}