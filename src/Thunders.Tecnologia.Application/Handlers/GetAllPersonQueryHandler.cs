using MediatR;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Application.Handlers;

public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, List<PersonDto>>
{
    private readonly IPersonService _personService;

    public GetAllPersonQueryHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<List<PersonDto>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
    {
        var enumerable = await _personService.GetAllAsync();
        return enumerable.ToList();
    }
}