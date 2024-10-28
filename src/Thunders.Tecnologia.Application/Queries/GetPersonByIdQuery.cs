using MediatR;
using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Queries;

public class GetPersonByIdQuery : IRequest<PersonDto?>
{
    public GetPersonByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}