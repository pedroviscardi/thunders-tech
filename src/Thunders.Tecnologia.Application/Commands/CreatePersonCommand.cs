using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class CreatePersonCommand : IRequest<Guid>
{
    public CreatePersonCommand(string name, string email, DateTime birthDate)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}