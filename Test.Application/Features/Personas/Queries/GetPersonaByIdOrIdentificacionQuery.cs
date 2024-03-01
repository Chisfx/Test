using Test.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Domain.DTOs;
namespace Test.Application.Features.Personas.Queries
{
    public class GetPersonaByIdOrIdentificacionQuery : IRequest<Result<PersonaModel>>
    {
        public int Id { get; set; }
        public string? Identificacion { get; set; }

        public class GetPersonaByIdOrIdentificacionQueryHandler : IRequestHandler<GetPersonaByIdOrIdentificacionQuery, Result<PersonaModel>>
        {
            private readonly IRepositoryAsync<Persona> _repository;
            private readonly IMapper _mapper;
            private readonly ILogger<GetPersonaByIdOrIdentificacionQuery> _logger;

            public GetPersonaByIdOrIdentificacionQueryHandler(
                IRepositoryAsync<Persona> repository,
                IMapper mapper,
                ILogger<GetPersonaByIdOrIdentificacionQuery> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Result<PersonaModel>> Handle(GetPersonaByIdOrIdentificacionQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    PersonaModel result = null;
                    string msg = string.Empty;

                    Persona entity = null;

                    if (request.Id > 0)
                    {
                        entity = await _repository.GetByIdAsync(request.Id);
                    }
                    else if (!string.IsNullOrEmpty(request.Identificacion))
                    {
                        entity = await _repository.GetFirstOrDefaultAsync(x => x.Identificacion.Equals(request.Identificacion));
                    }

                    if (entity == null)
                    {
                        msg = "Persona no encontrada.";
                    }
                    else
                    {
                        result = _mapper.Map<PersonaModel>(entity);
                    }

                    return Result<PersonaModel>.Success(result, msg);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return Result<PersonaModel>.Fail(ex.Message);
                }
            }
        }
    }
}
