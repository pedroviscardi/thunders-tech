﻿using MediatR;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Queries;

namespace Thunders.Tecnologia.Application.Handlers;

public class GetByIdPersonQueryHandler : IRequestHandler<GetByIdPersonQuery, PersonDto?>
{
    private readonly IPersonService _service;

    public GetByIdPersonQueryHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<PersonDto?> Handle(GetByIdPersonQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}