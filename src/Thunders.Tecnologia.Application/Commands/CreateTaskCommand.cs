using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class CreateTaskCommand : IRequest<Guid>
{
    public CreateTaskCommand(Guid idPerson, string title, string description)
    {
        IdPerson = idPerson;
        Title = title;
        Description = description;
    }

    public Guid IdPerson { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}