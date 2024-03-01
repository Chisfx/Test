using Test.Domain.Entities;
using AutoMapper;
using Test.Domain.DTOs;
using Test.Application.Features.Facturas.Commands;
using System.Globalization;
namespace Test.Application.Mappings
{
    public class FacturaProfile : Profile
    {
        public FacturaProfile()
        {
            CreateMap<Factura, CreateFacturaCommand>();
            CreateMap<CreateFacturaCommand, Factura>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<FacturaModel, CreateFacturaCommand>()
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => decimal.Parse(src.Monto, new CultureInfo("en-US"))));
            CreateMap<CreateFacturaCommand, FacturaModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateFacturaCommand, Factura>().ReverseMap();

            CreateMap<FacturaModel, UpdateFacturaCommand>()
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => decimal.Parse(src.Monto, new CultureInfo("en-US"))));
            CreateMap<UpdateFacturaCommand, FacturaModel>();

            CreateMap<Factura, FacturaModel>()
                .ForMember(dest => dest.Persona, opt => opt.MapFrom(src => src.Persona))
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto.ToString("N2", new CultureInfo("en-US"))));
            CreateMap<FacturaModel, Factura>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
