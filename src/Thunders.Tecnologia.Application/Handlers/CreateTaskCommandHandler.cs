using AutoMapper;
using MediatR;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ITaskService _service;

    public CreateTaskCommandHandler(IMapper mapper, ITaskService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var create = _mapper.Map<TaskDto>(request);
        var id = await _service.AddAsync(create);
        return id;
    }
}