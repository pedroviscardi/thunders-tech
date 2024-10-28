using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Guid>
{
    private readonly ILogger<UpdatePersonCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonService _service;

    public UpdatePersonCommandHandler(ILogger<UpdatePersonCommandHandler> logger, IMapper mapper, IPersonService service)
    {
        _logger = logger;
        _mapper = mapper;
        _service = service;
    }

    public async Task<Guid> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating a new person");

        var update = _mapper.Map<PersonDto>(request);
        var id = await _service.AddAsync(update);

        _logger.LogInformation("Updated a new person with id: {Id}", id);

        return id;
    }
}