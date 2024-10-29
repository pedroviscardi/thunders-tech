using MediatR;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
{
    private readonly IPersonService _service;

    public DeletePersonCommandHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteAsync(request.Id);
        return result;
    }
}