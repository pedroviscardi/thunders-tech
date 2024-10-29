using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class DeleteTaskCommand : IRequest<bool>
{
    public DeleteTaskCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}