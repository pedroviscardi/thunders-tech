using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class DeletePersonCommand : IRequest<bool>
{
    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}