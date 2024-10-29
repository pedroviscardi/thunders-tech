using MediatR;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Application.Handlers;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
{
    private readonly ITaskService _service;

    public GetAllTasksQueryHandler(ITaskService service)
    {
        _service = service;
    }

    public async Task<List<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var enumerable = await _service.GetAllAsync(request.IdPerson);
        return enumerable.ToList();
    }
}