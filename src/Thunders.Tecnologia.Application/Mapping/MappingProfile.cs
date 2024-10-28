using AutoMapper;
using Thunders.Tecnologia.Application.Commands;
using Thunders.Tecnologia.Application.DTOs;
using Thunders.Tecnologia.Domain.Entities;

namespace Thunders.Tecnologia.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<CreatePersonCommand, PersonDto>();
        CreateMap<UpdatePersonCommand, PersonDto>();
    }
}