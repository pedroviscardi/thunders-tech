using MediatR;
using Thunders.Tecnologia.Application.DTOs;

namespace Thunders.Tecnologia.Application.Queries;

public class GetAllPersonQuery : IRequest<List<PersonDto>>
{
}