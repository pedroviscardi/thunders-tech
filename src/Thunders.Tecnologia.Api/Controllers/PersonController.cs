using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Api.Controllers;

/// <summary>
///     Controller responsible for managing person data.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonController" /> class.
    /// </summary>
    /// <param name="mediator">The mediator used for sending commands and queries.</param>
    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves the person data by the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <returns>The <see cref="IActionResult" /> containing the person data if found, otherwise a 404 Not Found response.</returns>
    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetPersonByIdQuery(id);
        var personDto = await _mediator.Send(query);

        if (personDto is null)
        {
            return NotFound();
        }

        return Ok(personDto);
    }

    /// <summary>
    ///     Retrieves all persons' data.
    /// </summary>
    /// <returns>An <see cref="IActionResult" /> containing a list of all persons.</returns>
    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllPersonQuery();
        var persons = await _mediator.Send(query);
        return Ok(persons);
    }

    /// <summary>
    ///     Creates a new person with the specified data.
    /// </summary>
    /// <param name="personDto">The data transfer object containing person details.</param>
    /// <returns>A 201 Created response with the location of the new person resource.</returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> Add([FromBody] PersonDto personDto)
    {
        var command = new CreatePersonCommand(personDto.Name, personDto.Email, personDto.DateOfBirth);
        var createdId = await _mediator.Send(command);
        return CreatedAtAction("GetById", new {id = createdId}, personDto);
    }

    /// <summary>
    ///     Updates the person data with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <param name="personDto">The updated data transfer object containing person details.</param>
    /// <returns>An <see cref="IActionResult" /> indicating the outcome of the update operation.</returns>
    [HttpPut("[action]/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PersonDto personDto)
    {
        if (id != personDto.Id)
        {
            return BadRequest("The ID in the URL must match the ID in the body.");
        }

        var command = new UpdatePersonCommand(personDto.Id, personDto.Name, personDto.Email, personDto.DateOfBirth);
        var result = await _mediator.Send(command);

        return result ? Accepted() : NotFound();
    }

    /// <summary>
    ///     Deletes the person with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person to delete.</param>
    /// <returns>An <see cref="IActionResult" /> indicating the outcome of the delete operation.</returns>
    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeletePersonCommand(id);
        var result = await _mediator.Send(command);

        return result ? Accepted() : NotFound();
    }
}