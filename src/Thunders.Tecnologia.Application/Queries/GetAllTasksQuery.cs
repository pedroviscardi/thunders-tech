using MediatR;
using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Queries;

public class GetAllTasksQuery : IRequest<List<TaskDto>>
{
    public GetAllTasksQuery(Guid idPerson)
    {
        IdPerson = idPerson;
    }

    public Guid IdPerson { get; set; }
}