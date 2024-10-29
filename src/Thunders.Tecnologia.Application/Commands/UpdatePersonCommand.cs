using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class UpdatePersonCommand : IRequest<bool>
{
    public UpdatePersonCommand(Guid id, string name, string email, DateTime dateOfBirth)
    {
        Id = id;
        Name = name;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}