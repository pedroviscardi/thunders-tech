using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
{
    private readonly ILogger<CreatePersonCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonService _service;

    public CreatePersonCommandHandler(ILogger<CreatePersonCommandHandler> logger, IMapper mapper, IPersonService service)
    {
        _logger = logger;
        _mapper = mapper;
        _service = service;
    }

    public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new person");

        var create = _mapper.Map<PersonDto>(request);
        var id = await _service.AddAsync(create);

        _logger.LogInformation("Created a new person with id: {Id}", id);

        return id;
    }
}