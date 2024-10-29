using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
{
    private readonly ILogger<DeletePersonCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonService _service;

    public DeletePersonCommandHandler(ILogger<DeletePersonCommandHandler> logger, IMapper mapper, IPersonService service)
    {
        _logger = logger;
        _mapper = mapper;
        _service = service;
    }

    public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting a person");

        var result = await _service.DeleteAsync(request.Id);

        _logger.LogInformation("Deleted a person with id: {Id} with successful result: {Result}", request.Id, result);

        return result;
    }
}