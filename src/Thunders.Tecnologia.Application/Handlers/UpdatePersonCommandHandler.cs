using AutoMapper;
using MediatR;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;

namespace Thunders.Tecnologia.Application.Handlers;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IPersonService _service;

    public UpdatePersonCommandHandler(IMapper mapper, IPersonService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var update = _mapper.Map<PersonDto>(request);
        var result = await _service.UpdateAsync(update);
        return result;
    }
}