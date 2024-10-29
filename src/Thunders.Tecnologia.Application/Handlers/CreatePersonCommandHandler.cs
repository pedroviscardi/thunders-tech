using AutoMapper;
using MediatR;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IPersonService _service;

    public CreatePersonCommandHandler(IMapper mapper, IPersonService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var create = _mapper.Map<PersonDto>(request);
        var id = await _service.AddAsync(create);
        return id;
    }
}