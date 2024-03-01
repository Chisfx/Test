using AspNetCoreHero.Results;
using AutoMapper;
using Bogus;
using Bogus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Domain.DTOs;
using Test.Domain.Entities;

namespace Test.Application.Features.Personas.Queries
{
    public class GetAllPersonasQuery : IRequest<Result<List<PersonaModel>>>
    {
        public bool Faker { get; set; }
        public string? Match { get; set; }
        public class GetAllPersonasQueryHandler : IRequestHandler<GetAllPersonasQuery, Result<List<PersonaModel>>>
        {
            private readonly IRepositoryAsync<Persona> _repository;
            private readonly IMapper _mapper;
            private readonly ILogger<GetAllPersonasQuery> _logger;

            public GetAllPersonasQueryHandler(
                IRepositoryAsync<Persona> repository,
                IMapper mapper,
                ILogger<GetAllPersonasQuery> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Result<List<PersonaModel>>> Handle(GetAllPersonasQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    List<PersonaModel> entities;

                    if (request.Faker)
                    {
                        var user = new Faker<PersonaModel>()
                            .RuleFor(c => c.Nombre, (k, a) => k.Name.FirstName().ClampLength(min: 3, max: 50))
                            .RuleFor(c => c.ApellidoPaterno, (k, a) => k.Name.LastName().ClampLength(min: 3, max: 50))
                            .RuleFor(c => c.ApellidoMaterno, (k, a) => k.Name.LastName().ClampLength(min: 3, max: 50))
                            .RuleFor(c => c.Identificacion, (k, a) => k.Random.String().ClampLength(min: 3, max: 50));

                        entities = user.Generate(100);
                    }
                    else
                    {
                        List<Persona> response;
                        if (string.IsNullOrEmpty(request.Match))
                        {
                            response = await _repository.GetAllAsync();
                        }
                        else
                        {
                            response = await _repository.GetAllAsync(c => 
                            c.Nombre.Contains(request.Match, StringComparison.OrdinalIgnoreCase) ||
                            c.ApellidoPaterno.Contains(request.Match, StringComparison.OrdinalIgnoreCase) ||
                            c.ApellidoMaterno.Contains(request.Match, StringComparison.OrdinalIgnoreCase) ||
                            c.Identificacion.Contains(request.Match, StringComparison.OrdinalIgnoreCase));
                        }
                        entities = _mapper.Map<List<PersonaModel>>(response);
                    }

                    return await Result<List<PersonaModel>>.SuccessAsync(entities);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return await Result<List<PersonaModel>>.FailAsync(ex.Message);
                }
            }
        }
    }
}
