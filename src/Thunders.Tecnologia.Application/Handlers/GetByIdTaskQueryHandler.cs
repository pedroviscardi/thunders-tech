using MediatR;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Application.Handlers;

public class GetByIdTaskQueryHandler : IRequestHandler<GetByIdTaskQuery, TaskDto?>
{
    private readonly ITaskService _service;

    public GetByIdTaskQueryHandler(ITaskService service)
    {
        _service = service;
    }

    public async Task<TaskDto?> Handle(GetByIdTaskQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}