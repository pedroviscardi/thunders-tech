using MediatR;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Application.Handlers;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto?>
{
    private readonly IPersonService _personService;

    public GetPersonByIdQueryHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<PersonDto?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        return await _personService.GetByIdAsync(request.Id);
    }
}