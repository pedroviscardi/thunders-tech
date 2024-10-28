using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Thunders.Tecnologia.Api.Controllers;

/// <summary>
///     People controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IMediator _mediator;

    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public PeopleController(ILogger<PeopleController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    ///     Get all people
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Fetching all people");
        // Your logic to get people
        return Ok();
    }
}