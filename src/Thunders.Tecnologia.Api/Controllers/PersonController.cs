using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Api.Controllers;

/// <summary>
///     Person controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="mediator"></param>
    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get person data by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
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
    ///     Get all persons
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllPersonQuery();
        var persons = await _mediator.Send(query);
        return Ok(persons);
    }

    /// <summary>
    ///     Create person data
    /// </summary>
    /// <param name="personDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PersonDto personDto)
    {
        var command = new CreatePersonCommand(personDto.Name, personDto.Email, personDto.DateOfBirth);
        var createdId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {createdId}, personDto);
    }

    /// <summary>
    ///     Update data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="personDto"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
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
    ///     Delete person
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeletePersonCommand(id);
        var result = await _mediator.Send(command);

        return result ? Accepted() : NotFound();
    }
}