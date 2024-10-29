using AutoMapper;
using MediatR;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly ITaskService _service;

    public UpdateTaskCommandHandler(IMapper mapper, ITaskService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var update = _mapper.Map<TaskDto>(request);
        var result = await _service.UpdateAsync(update);
        return result;
    }
}