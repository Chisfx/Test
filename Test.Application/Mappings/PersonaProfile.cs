using Test.Domain.Entities;
using AutoMapper;
using Test.Domain.DTOs;
using Test.Application.Features.Personas.Commands;
namespace Test.Application.Mappings
{
    public class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            CreateMap<Persona, CreatePersonaCommand>();
            CreateMap<CreatePersonaCommand, Persona>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<PersonaModel, CreatePersonaCommand>();
            CreateMap<CreatePersonaCommand, PersonaModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdatePersonaCommand, Persona>().ReverseMap();

            CreateMap<UpdatePersonaCommand, PersonaModel>().ReverseMap();

            CreateMap<Persona, PersonaModel>();
            CreateMap<PersonaModel, Persona>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
