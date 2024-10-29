using MediatR;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly ITaskService _service;

    public DeleteTaskCommandHandler(ITaskService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteAsync(request.Id);
        return result;
    }
}