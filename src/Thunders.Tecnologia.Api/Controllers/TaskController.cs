using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Api.Controllers;

/// <summary>
///     Controller responsible for managing Task data.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TaskController" /> class.
    /// </summary>
    /// <param name="mediator">The mediator used for sending commands and queries.</param>
    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves the Task data by the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Task.</param>
    /// <returns>The <see cref="IActionResult" /> containing the Task data if found, otherwise a 404 Not Found response.</returns>
    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetByIdTaskQuery(id);
        var taskDto = await _mediator.Send(query);

        if (taskDto is null)
        {
            return NotFound();
        }

        return Ok(taskDto);
    }

    /// <summary>
    ///     Retrieves all Tasks' data.
    /// </summary>
    /// <returns>An <see cref="IActionResult" /> containing a list of all Tasks.</returns>
    [HttpGet("get/all/{idPerson:guid}")]
    public async Task<IActionResult> GetAll(Guid idPerson)
    {
        var query = new GetAllTasksQuery(idPerson);
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }

    /// <summary>
    ///     Creates a new Task with the specified data.
    /// </summary>
    /// <param name="request">The data transfer object containing Task details.</param>
    /// <returns>A 201 Created response with the location of the new Task resource.</returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> Add([FromBody] TaskDto request)
    {
        var command = new CreateTaskCommand(request.IdPerson, request.Title, request.Description);
        var createdId = await _mediator.Send(command);
        return CreatedAtAction("GetById", new {id = createdId}, request);
    }

    /// <summary>
    ///     Updates the Task data with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Task.</param>
    /// <param name="request">The updated data transfer object containing Task details.</param>
    /// <returns>An <see cref="IActionResult" /> indicating the outcome of the update operation.</returns>
    [HttpPut("[action]/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TaskDto request)
    {
        if (id != request.Id)
        {
            return BadRequest("The ID in the URL must match the ID in the body.");
        }

        var command = new UpdateTaskCommand(request.Id, request.Title, request.Description);
        var result = await _mediator.Send(command);

        return result ? Accepted() : NotFound();
    }

    /// <summary>
    ///     Deletes the Task with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Task to delete.</param>
    /// <returns>An <see cref="IActionResult" /> indicating the outcome of the delete operation.</returns>
    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTaskCommand(id);
        var result = await _mediator.Send(command);

        return result ? Accepted() : NotFound();
    }
}