using Test.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Domain.DTOs;
namespace Test.Application.Features.Facturas.Queries
{
    public class GetFacturaByPersonaIdQuery : IRequest<Result<FacturaModel>>
    {
        public int PersonaId { get; set; }

        public class GetFacturaByPersonaIdQueryHandler : IRequestHandler<GetFacturaByPersonaIdQuery, Result<FacturaModel>>
        {
            private readonly IRepositoryAsync<Factura> _repository;
            private readonly IMapper _mapper;
            private readonly ILogger<GetFacturaByPersonaIdQuery> _logger;

            public GetFacturaByPersonaIdQueryHandler(
                IRepositoryAsync<Factura> repository,
                IMapper mapper,
                ILogger<GetFacturaByPersonaIdQuery> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Result<FacturaModel>> Handle(GetFacturaByPersonaIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    FacturaModel result = null;
                    string msg = string.Empty;

                    var entity = await _repository.GetFirstOrDefaultAsync(x => x.PersonaId.Equals(request.PersonaId));

                    if (entity == null)
                    {
                        msg = "User not found.";
                    }
                    else
                    {
                        result = _mapper.Map<FacturaModel>(entity);
                    }

                    return Result<FacturaModel>.Success(result, msg);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return Result<FacturaModel>.Fail(ex.Message);
                }
            }
        }
    }
}
