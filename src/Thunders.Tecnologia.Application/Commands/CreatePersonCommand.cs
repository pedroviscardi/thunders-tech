using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class CreatePersonCommand : IRequest<Guid>
{
    public CreatePersonCommand(string name, string email, DateTime dateOfBirth)
    {
        Name = name;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}