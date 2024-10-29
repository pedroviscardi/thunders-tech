using MediatR;
using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Queries;

public class GetByIdPersonQuery : IRequest<PersonDto?>
{
    public GetByIdPersonQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}