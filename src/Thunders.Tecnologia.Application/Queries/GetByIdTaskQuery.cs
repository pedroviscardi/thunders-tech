using MediatR;
using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Queries;

public class GetByIdTaskQuery : IRequest<TaskDto?>
{
    public GetByIdTaskQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}