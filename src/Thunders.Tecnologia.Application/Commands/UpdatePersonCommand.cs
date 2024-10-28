﻿using MediatR;

namespace Thunders.Tecnologia.Application.Commands;

public class UpdatePersonCommand : IRequest<Guid>
{
    public UpdatePersonCommand(Guid id, string name, string email, DateTime birthDate)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}