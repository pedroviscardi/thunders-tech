using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class UpdateTaskCommand : IRequest<bool>
{
    public UpdateTaskCommand(Guid id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}