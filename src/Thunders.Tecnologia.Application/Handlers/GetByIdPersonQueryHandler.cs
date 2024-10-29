using MediatR;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Application.Handlers;

public class GetByIdPersonQueryHandler : IRequestHandler<GetByIdPersonQuery, PersonDto?>
{
    private readonly IPersonService _personService;

    public GetByIdPersonQueryHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<PersonDto?> Handle(GetByIdPersonQuery request, CancellationToken cancellationToken)
    {
        return await _personService.GetByIdAsync(request.Id);
    }
}